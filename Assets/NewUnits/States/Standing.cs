namespace Assets.NewUnits.States
{
    public class Standing : UnitState
    {
        public Standing(MovementModule movement, TargetingModule targeting,
            UnitControlParameters parameters, UnitStates states)
            : base(movement, targeting, parameters, states)
        {
        }

        public override void FixedUpdate()
        {
            if (Parameters.IsLookGlobalDirectionSet)
            {
                CalculateLimiter();
                ManageReachingMovementTurnDirection(Parameters.LookGlobalDirection);
                ManageReachingTargetingTurnDirection();
            }
        }
    }
}