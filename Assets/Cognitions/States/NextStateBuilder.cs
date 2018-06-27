namespace Assets.Cognitions.States
{
    public class NextStateBuilder : IExtendedStateBuilder
    {
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