using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public class ComputerStatesFactory
    {
        private readonly IMap _map;
        private readonly MovementHelper _movementHelper;
        private readonly TargetingHelper _targetingHelper;
        private readonly IUnit _unit;
        private readonly IVisionObserver _vision;

        public ComputerStatesFactory(IMap map, IUnit unit, IVisionObserver vision,
            MovementHelper movementHelper,
            TargetingHelper targetingHelper)
        {
            _map = map;
            _unit = unit;
            _vision = vision;
            _movementHelper = movementHelper;
            _targetingHelper = targetingHelper;
        }

        public Watching CreateWatching(Vector3? lookDirection, float timeLimit = 0)
        {
            return new Watching(_movementHelper, _targetingHelper, _unit, _map, _vision, lookDirection,
                timeLimit);
        }

        public Chasing CreateChasing(ITarget target)
        {
            return new Chasing(_movementHelper, _targetingHelper, _unit, _map, _vision, target);
        }

        public Strafing CreateStrafing(ITarget target)
        {
            return new Strafing(_movementHelper, _targetingHelper, _unit, _map, _vision, target);
        }

        public Searching CreateSearching(ITarget target)
        {
            return new Searching(_movementHelper, _targetingHelper, _unit, _map, _vision, target);
        }

        public Moving CreateMoving(Vector3 targetPosition)
        {
            return new Moving(_movementHelper, _targetingHelper, _unit, _map, _vision, targetPosition);
        }
    }
}