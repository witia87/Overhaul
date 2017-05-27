using System;
using System.Collections.Generic;
using Assets.Cores;
using Assets.Map.Nodes;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Brain.States
{
    public class ChasingEnemy : HumanoidCognitionState
    {
        private readonly Core _target;
        private Vector3 _movementDirection;
        private List<INode> _path;
        private Vector3 _targetLastSeenDirection;

        private Vector3 _targetLastSeenPosition;

        private int _wasForwardLastPressed = 1;

        public ChasingEnemy(Cognition<HumanoidCognitionStateIds> parrentCognition, Core target)
            : base(parrentCognition, HumanoidCognitionStateIds.ChasingEnemy)
        {
            _target = target;
            _targetLastSeenPosition = target.gameObject.transform.position;
            _targetLastSeenDirection = _target.Rigidbody.velocity.normalized;
            _path = PathFinder.FindPath(Core.gameObject.transform.position,
                _target.gameObject.transform.position);
        }

        public override ICognitionState<HumanoidCognitionStateIds> Update()
        {
            if (MountedModules.AreTurretControlsMounted)
            {
                foreach (var turret in MountedModules.TurretControls)
                {
                    if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.01f))
                    {
                        if (turret.VisibleCores.Contains(_target))
                        {
                            _targetLastSeenPosition = _target.gameObject.transform.position;
                            _targetLastSeenDirection = _target.Rigidbody.velocity.normalized;
                            _path = PathFinder.FindPath(Core.gameObject.transform.position,
                                _target.gameObject.transform.position);
                        }
                        else
                        {
                            if (ProbabilisticTriggering.TestProbabilisty(0.95f))
                            {
                                var runaroundPosition = _targetLastSeenPosition + _targetLastSeenDirection*
                                                        (_targetLastSeenPosition - Core.gameObject.transform.position)
                                                            .magnitude;
                                runaroundPosition = ClampVector(runaroundPosition);
                                INode node;
                                if (GameMechanics.Stores.MapStore.TryGetNode(runaroundPosition, Scale, out node))
                                {
                                    return new Investigate(ParrentCognition, node.Position,
                                        -_targetLastSeenDirection);
                                }
                            }
                            return new Investigate(ParrentCognition, _targetLastSeenPosition,
                                _targetLastSeenDirection);
                        }
                    }
                }
                ManageTurret();
            }

            if (MountedModules.IsHumanoidMovementControlMounted)
            {
                ManageMovement();
            }

            return this;
        }

        protected virtual void ManageTurret()
        {
            foreach (var turretControl in MountedModules.TurretControls)
            {
                var direction = _target.gameObject.transform.position - turretControl.SightPosition;
                var distance = direction.magnitude;
                if (distance > 1)
                {
                    var angle = Vector3.Angle(direction, turretControl.TurretDirection);
                    if (angle < 0)
                    {
                        throw new ApplicationException("Angle is less than 0.");
                    }
                    if (angle > 3)
                    {
                        turretControl.TurnTowards(direction);
                    }
                    if (angle < 10)
                    {
                        turretControl.Fire();
                    }
                }
            }
        }

        protected virtual void ManageMovement()
        {
            INode currentNode;
            GameMechanics.Stores.MapStore.TryGetNode(Core.gameObject.transform.position, Scale, out currentNode);

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
                       GameMechanics.Stores.MapStore.IsRectangleClear(
                           currentNode, _path[shortcutLength]))
                {
                    shortcutLength++;
                }
                var localPathDirection = Core.gameObject.transform.InverseTransformDirection(
                    _path[shortcutLength].Position - Core.gameObject.transform.position);
                localPathDirection.Normalize();

                _wasForwardLastPressed = localPathDirection.z > 0
                    ? 1
                    : localPathDirection.z < 0 ? -1 : _wasForwardLastPressed;
                if (localPathDirection.x*_wasForwardLastPressed > 0)
                {
                    //MountedModules.HumanoidMovementControl.TurnLeft();
                }
                else
                {
                    //MountedModules.HumanoidMovementControl.TurnRight();
                }

                var angle = Vector3.Angle(Vector3.forward, localPathDirection);
                if (angle < 45)
                {
                    MountedModules.HumanoidMovementControl.Move(new Vector3(0, 0, localPathDirection.z));
                }
                _movementDirection = Core.gameObject.transform.TransformDirection(localPathDirection);
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