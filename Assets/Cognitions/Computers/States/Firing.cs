using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Units;
using Assets.Utilities;
using Assets.Vision;

namespace Assets.Cognitions.Computers.States
{
    public class Firing : ComputerState
    {
        private readonly ITarget _target;

        public Firing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map,
            ITarget target)
            : base(ComputerStateIds.Firing, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            if (Map.IsPositionDangorous(Unit.LogicPosition))
            {
                return RememberCurrent().AndChangeStateTo(StatesFactory.CreateStrafing(_target));
            }

            if (_target.IsVisible)
            {
                if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
                {
                    var distanceToTarget = (_target.Position - Unit.LogicPosition).magnitude;
                    if (distanceToTarget > Unit.Gun.EfectiveRange.y)
                    {
                        return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(_target));
                    }

                    if (distanceToTarget < Unit.Gun.EfectiveRange.x)
                    {
                        return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateBacking(_target));
                    }
                }
                TargetingHelper.ManageAimingAtTheTarget(_target);
                return this;
            }

            return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateSearching(_target));
        }
    }
}