using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using Assets.Utilities;

namespace Assets.Cognitions.Computers.States
{
    public class Firing : ComputerState
    {
        private readonly ITarget _target;

        public Firing(MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map, ITarget target)
            : base(ComputerStateIds.Firing, movementHelper, targetingHelper, unit, map)
        {
            _target = target;
        }

        public override CognitionState<ComputerStateIds> Update()
        {
            Unit.Movement.StopMoving();
            if (ProbabilisticTriggering.TestOnAverageOnceEvery(0.1f))
            {
                var distanceToTarget = (_target.LastSeenPosition - Unit.gameObject.transform.position).magnitude;
                if (!_target.IsVisible)
                {
                    return DisposeCurrent().AndReturnToThePreviousState();
                }

                if (distanceToTarget > Unit.Targeting.Gun.EfectiveRange.y)
                {
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateChasing(_target));
                }

                if (distanceToTarget < Unit.Targeting.Gun.EfectiveRange.x)
                {
                    return DisposeCurrent().AndChangeStateTo(StatesFactory.CreateBacking(_target));
                }
            }

            TargetingHelper.ManageAimingAtTheTarget(_target);
            return this;
        }
    }
}