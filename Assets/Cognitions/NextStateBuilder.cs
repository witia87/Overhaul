namespace Assets.Cognitions
{
    public class NextStateBuilder<TStateIds> : IExtendedStateBuilder<TStateIds>
    {
        private readonly CognitionState<TStateIds> _parentState;

        public NextStateBuilder(CognitionState<TStateIds> parentState)
        {
            _parentState = parentState;
        }

        public CognitionState<TStateIds> AndChangeStateTo(CognitionState<TStateIds> nextState)
        {
            return nextState;
        }

        public CognitionState<TStateIds> AndReturnToThePreviousState()
        {
            return null;
        }
    }
}