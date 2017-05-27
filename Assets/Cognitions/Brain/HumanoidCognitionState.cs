namespace Assets.Cognitions.Brain
{
    public abstract class HumanoidCognitionState : CognitionState<HumanoidCognitionStateIds>
    {
        protected HumanoidCognitionState(Cognition<HumanoidCognitionStateIds> parrentCognition,
            HumanoidCognitionStateIds id)
            : base(parrentCognition, id)
        {
        }
    }
}