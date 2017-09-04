namespace Assets.NewUnits.States
{
    public class MovingForward : UnitState
    {
        public MovingForward(MovementModule movement, TargetingModule targeting, UnitControlParameters parameters,
            UnitStates states) :
            base(movement, targeting, parameters, states)
        {
        }

        public override void FixedUpdate()
        {
            if (Parameters.IsLookGlobalDirectionSet)
            {
                CalculateLimiter();
                ManageReachingMovementTurnDirection(
                    ((Parameters.LookGlobalDirection + Parameters.MoveGlobalDirection) / 2).normalized);
                ManageReachingTargetingTurnDirection();
                Movement.AddAcceleration(Parameters.MoveGlobalDirection);
            }
        }
    }
}