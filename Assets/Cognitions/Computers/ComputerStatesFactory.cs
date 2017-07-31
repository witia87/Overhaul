using Assets.Cognitions.Computers.States;
using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using UnityEngine;

namespace Assets.Cognitions.Computers
{
    public class ComputerStatesFactory
    {
        private readonly IMap _map;
        private readonly MovementHelper _movementHelper;
        private readonly TargetingHelper _targetingHelper;
        private readonly IUnitControl _unit;

        public ComputerStatesFactory(IMap map, IUnitControl unit, MovementHelper movementHelper,
            TargetingHelper targetingHelper)
        {
            _map = map;
            _unit = unit;
            _movementHelper = movementHelper;
            _targetingHelper = targetingHelper;
        }

        public Watching CreateWatching(Vector3? lookDirection, float timeLimit = 0)
        {
            return new Watching(_movementHelper, _targetingHelper, _unit, _map, lookDirection, timeLimit);
        }

        public Chasing CreateChasing(ITarget target)
        {
            return new Chasing(_movementHelper, _targetingHelper, _unit, _map, target);
        }

        public Firing CreateFiring(ITarget target)
        {
            return new Firing(_movementHelper, _targetingHelper, _unit, _map, target);
        }

        public Strafing CreateStrafing(ITarget target)
        {
            return new Strafing(_movementHelper, _targetingHelper, _unit, _map, target);
        }

        public Searching CreateSearching(ITarget target)
        {
            return new Searching(_movementHelper, _targetingHelper, _unit, _map, target);
        }

        public Moving CreateMoving(Vector3 targetPosition)
        {
            return new Moving(_movementHelper, _targetingHelper, _unit, _map, targetPosition);
        }

        public Backing CreateBacking(ITarget target)
        {
            return new Backing(_movementHelper, _targetingHelper, _unit, _map, target);
        }
    }
}