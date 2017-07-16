using System;
using System.Collections.Generic;
using Assets.Map.Nodes;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Cognitions.Computers.States
{
    public class ChasingEnemy : ComputerState
    {
        private readonly Unit _target;
        private List<INode> _path;
        private Vector3 _targetLastSeenDirection;

        private Vector3 _targetLastSeenPosition;

        public ChasingEnemy(Cognition<ComputerStateIds> parrentCognition, Unit target)
            : base(parrentCognition, ComputerStateIds.ChasingEnemy)
        {
            _target = target;
            _targetLastSeenPosition = target.gameObject.transform.position;
            _targetLastSeenDirection = _target.Rigidbody.velocity.normalized;
            _path = PathFinder.FindPath(Unit.gameObject.transform.position,
                _target.gameObject.transform.position);
        }

        public override ICognitionState<ComputerStateIds> Update()
        {
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                if (Unit.Targeting.VisionSensor.VisibleUnits.Contains(_target))
                {
                    _targetLastSeenPosition = _target.gameObject.transform.position;
                    _targetLastSeenDirection = _target.Rigidbody.velocity.normalized;
                    _path = PathFinder.FindPath(Unit.gameObject.transform.position,
                        _target.gameObject.transform.position);
                    ManageTargetingTheUnit(_target);
                    ManageMovingAlongThePath(ref _path);
                }
                else
                {
                    Unit.Targeting.Gun.StopFiring();
                    Dispose();
                    if (ProbabilisticTriggering.TestProbabilisty(0.5f))
                    {
                        var runaroundPosition = _targetLastSeenPosition + _targetLastSeenDirection*
                                                (_targetLastSeenPosition - Unit.gameObject.transform.position)
                                                    .magnitude;
                        runaroundPosition = ClampVector(runaroundPosition);
                        INode node;
                        if (MapStore.TryGetNode(runaroundPosition, Scale, out node))
                        {
                            return new Investigate(ParentCognition, node.Position,
                                -_targetLastSeenDirection);
                        }
                    }
                    return new Investigate(ParentCognition, _targetLastSeenPosition,
                        _targetLastSeenDirection);
                }
            }
            return this;
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