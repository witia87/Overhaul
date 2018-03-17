using Assets.Cognitions.Computers.States;
using Assets.Cognitions.Helpers;

namespace Assets.Cognitions.Computers
{
    public class Computer : Cognition<ComputerStateIds>
    {
        protected override void Start()
        {
            base.Start();
            DefaultState = new Watching(new MovementHelper(UnitControl, Map), new TargetingHelper(UnitControl, Map), UnitControl, Map, null);
        }
    }
}