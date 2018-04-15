namespace Assets.Cognitions.States
{
    public class NextStateBuilder : IExtendedStateBuilder
    {
        private readonly CognitionState _parentState;

        public NextStateBuilder(CognitionState parentState)
        {
            _parentState = parentState;
        }

        public CognitionState AndChangeStateTo(CognitionState nextState)
        {
            return nextState;
        }

        public CognitionState AndReturnToThePreviousState()
        {
            return null;
        }
    }
}