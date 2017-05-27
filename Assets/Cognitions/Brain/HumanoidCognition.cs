using Assets.Cognitions.Brain.States;

namespace Assets.Cognitions.Brain
{
    public class HumanoidCognition : Cognition<HumanoidCognitionStateIds>
    {
        protected override void Start()
        {
            base.Start();
            CurrentState = new Idle(this);
        }
    }
}