namespace Assets.NewUnits.States
{
    public class UnitStates
    {
        public UnitStates(MovementModule movementModule, TargetingModule targetingModule,
            UnitControlParameters parameters)
        {
            Standing = new Standing(movementModule, targetingModule, parameters, this);
            MovingForward = new MovingForward(movementModule, targetingModule, parameters, this);
            MovingBackward = new MovingBackward(movementModule, targetingModule, parameters, this);
        }

        public Standing Standing { get; private set; }
        public MovingForward MovingForward { get; private set; }
        public MovingBackward MovingBackward { get; private set; }
    }
}