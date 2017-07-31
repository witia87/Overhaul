using Assets.Cognitions.Computers.States;
using Assets.Cognitions.Helpers;
using UnityEngine;

namespace Assets.Cognitions.Computers
{
    public class Computer : Cognition<ComputerStateIds>
    {
        protected override void Start()
        {
            base.Start();
            DefaultState = new Watching(new MovementHelper(Unit, Map), new TargetingHelper(Unit, Map), Unit, Map, null);
        }
    }
}