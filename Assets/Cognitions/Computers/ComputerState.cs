using System.Collections.Generic;
using Assets.Map.Nodes;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Computers
{
    public abstract class ComputerState : CognitionState<ComputerStateIds>
    {
        protected ComputerState(Cognition<ComputerStateIds> parentCognition,
            ComputerStateIds id)
            : base(parentCognition, id)
        {
        }

        protected void ManageMovingAlongThePath(ref List<INode> path)
        {
            INode currentNode;
            MapStore.TryGetNode(Unit.gameObject.transform.position, Scale, out currentNode);
            
            if (path != null)
            {
                var index = path.LastIndexOf(currentNode);
                if (index >= 0)
                {
                    path.RemoveRange(0, index + 1);
                }
                if (path.Count == 0)
                {
                    path = null;
                }
            }

            if (path != null)
            {
                var shortcutLength = 0;
                while (shortcutLength < path.Count - 1 &&
                       MapStore.IsRectangleClear(
                           currentNode, path[shortcutLength]))
                {
                    shortcutLength++;
                }

                Unit.Movement.GoTo(path[shortcutLength].Position);
            }
            else
            {
                Unit.Movement.StopMoving();
            }
        }

        protected void ManageTargetingTheUnit(Unit target)
        {
            Unit.Targeting.LookAt(target.Targeting.Center);
            if (Unit.Targeting.IsGunMounted)
            {
                if (Vector3.Angle(target.Targeting.Center -
                    Unit.Targeting.Gun.FirePosition, Unit.Targeting.Gun.FireDirection) < 10)
                {
                    Unit.Targeting.Gun.Fire();
                }
                else
                {
                    Unit.Targeting.Gun.StopFiring();
                }
            }
        }

        protected virtual Unit GetHighestPriorityTarget()
        {
            Unit highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            if (Unit.Targeting != null)
            {
                var visionSensor = Unit.Targeting.VisionSensor;
                foreach (var testedUnit in visionSensor.VisibleUnits)
                {
                    var currentDistance =
                        (testedUnit.transform.position - visionSensor.SightPosition).magnitude;
                    if (currentDistance < minDistance)
                    {
                        highestPriorityTargetSoFar = testedUnit;
                        minDistance = currentDistance;
                    }
                }
            }
            return highestPriorityTargetSoFar;
        }


        protected Vector3 ClampVector(Vector3 v)
        {
            return new Vector3(
                Mathf.Min(MapStore.MapWidth, Mathf.Max(0, v.x)),
                0,
                Mathf.Min(MapStore.MapLength, Mathf.Max(0, v.z)));
        }
    }
}