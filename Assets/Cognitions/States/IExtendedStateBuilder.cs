namespace Assets.Cognitions.States
{
    public interface IExtendedStateBuilder : IStateBuilder
    {
        CognitionState AndReturnToThePreviousState();
    }
}