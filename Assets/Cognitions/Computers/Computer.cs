using Assets.Cognitions.Computers.States;

namespace Assets.Cognitions.Computers
{
    public class Computer : Cognition<ComputerStateIds>
    {
        protected override void Start()
        {
            DefaultState = new Idle(this);
            base.Start();
        }
    }
}