using System.Collections.Generic;
using Assets.Map.Nodes;
using Assets.Modules.Turrets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Brain.States
{
    public class Investigate : CognitionState<HumanoidCognitionStateIds>
    {
        private readonly Vector3 _targetLastSeenDirection;

        private readonly Vector3 _targetLastSeenPosition;
        private List<INode> _path;

        public Investigate(Cognition<HumanoidCognitionStateIds> parrentCognition, Vector3 targetLastSeenPosition,
            Vector3 targetLastSeenDirection)
            : base(parrentCognition, HumanoidCognitionStateIds.Investigate)
        {
            _targetLastSeenPosition = targetLastSeenPosition;
            _targetLastSeenDirection = targetLastSeenDirection;
            if (targetLastSeenDirection.magnitude > 0)
            {
                _targetLastSeenDirection.Normalize();
            }
            _path = PathFinder.FindPath(Core.gameObject.transform.position,
                _targetLastSeenPosition);
        }

        public override ICognitionState<HumanoidCognitionStateIds> Update()
        {
            if (MountedModules.AreTurretControlsMounted)
            {
                foreach (var turret in MountedModules.TurretControls)
                {
                    ProbabilisticTriggering.PerformOnAverageOnceEvery(5, () => ChangeTurretDircetion(turret));
                    if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.5f))
                    {
                        if (turret.VisibleCores.Count > 0)
                        {
                            var target = GetHighestPriorityTarget();
                            return new ChasingEnemy(ParrentCognition, target);
                        }
                    }
                }
            }

            if (MountedModules.IsHumanoidMovementControlMounted)
            {
                if (ProbabilisticTriggering.TestOnAverageOnceEvery(1))
                {
                    _path = PathFinder.FindPath(Core.gameObject.transform.position,
                        _targetLastSeenPosition);
                }
                ManageMovement();
            }

            if (_path == null || _path.Count == 0)
            {
                return new Idle(ParrentCognition, _targetLastSeenDirection);
            }
            return this;
        }

        private void ChangeTurretDircetion(ITurretControl turret)
        {
            turret.LookAt(_targetLastSeenPosition);
        }

        protected virtual void ManageMovement()
        {
            INode currentNode;
            MapStore.TryGetNode(Core.gameObject.transform.position, Scale, out currentNode);

            DrawArrow.ForDebug(currentNode.Position,
                Vector3.up*2, Color.yellow, 0.1f, 0);
            if (_path != null)
            {
                var index = _path.LastIndexOf(currentNode);
                if (index >= 0)
                {
                    _path.RemoveRange(0, index + 1);
                }
                if (_path.Count == 0)
                {
                    _path = null;
                }
            }

            if (_path != null)
            {
                var shortcutLength = 0;
                while (shortcutLength < _path.Count - 1 &&
                       MapStore.IsRectangleClear(
                           currentNode, _path[shortcutLength]))
                {
                    shortcutLength++;
                }
                var localPathDirection = Core.gameObject.transform.InverseTransformDirection(
                    _path[shortcutLength].Position - Core.gameObject.transform.position);
                localPathDirection.Normalize();
                // MountedModules.MovementControl.Turn(localPathDirection.x *
                //                                     Vector3.Dot(MountedModules.MovementControl.MovementDirection,
                //                                         MountedModules.MovementControl.BodyDirection));

                if (Vector3.Angle(Vector3.forward, localPathDirection) > 60)
                {
                    // MountedModules.MovementControl.Move(new Vector3(0, 0, localPathDirection.z));
                }
            }
        }

        public override void OnDrawGizmos()
        {
            if (_path != null)
            {
                for (var i = 0; i < _path.Count - 1; i++)
                {
                    DrawArrow.ForDebug(_path[i].Position + Vector3.up/100,
                        _path[i + 1].Position - _path[i].Position, Color.green, 0.1f, 0);
                }
            }
        }
    }
}