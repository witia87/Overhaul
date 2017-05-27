using Assets.Cognitions.VehicleCognitions.States;

namespace Assets.Cognitions.VehicleCognitions
{
    public class VehicleCognition : Cognition<VehicleCognitionStateIds>
    {
        protected override void Start()
        {
            base.Start();
            CurrentState = new Idle(this);
        }
    }
}