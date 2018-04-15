namespace Assets.Cognitions.States
{
    public interface IStateBuilder
    {
        CognitionState AndChangeStateTo(CognitionState nextState);
    }
}