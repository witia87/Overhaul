using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Modules;
using Assets.Modules.Movement;
using Assets.Modules.Turrets;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds> : ICognitionState<TStateIds>
    {
        protected readonly IMapStore MapStore;
        protected readonly Cognition<TStateIds> ParrentCognition;

        protected CognitionState(Cognition<TStateIds> parrentCognition, TStateIds id)
        {
            Id = id;
            ParrentCognition = parrentCognition;
            MapStore = parrentCognition.MapStore;
        }

        protected IPathFinder PathFinder
        {
            get { return ParrentCognition.PathFinder; }
        }

        protected IMovementControl MovementControl
        {
            get { return ParrentCognition.MovementModule; }
        }

        protected ITurretControl TurretControl
        {
            get { return ParrentCognition.TurretModule; }
        }

        protected int Scale
        {
            get { return ParrentCognition.Scale; }
        }

        public TStateIds Id { get; private set; }

        public abstract ICognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        protected virtual Module GetHighestPriorityTarget()
        {
            Module highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            if (TurretControl != null)
            {
                var visionSensor = TurretControl.VisionSensor;
                foreach (var testedModule in visionSensor.VisibleModules)
                {
                    var currentDistance =
                        (testedModule.transform.position - visionSensor.SightPosition).magnitude;
                    if (currentDistance < minDistance)
                    {
                        highestPriorityTargetSoFar = testedModule;
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