using Assets.Environment.Units.Bodies.Coordinator.States.Base;

namespace Assets.Environment.Units.Bodies.Coordinator.States
{
    public class StandingUp : UnitState
    {
        public StandingUp(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
            : base(torso, legs, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            Legs.Crouch();
            Legs.StraightenUp();
            Torso.StraightenUp();
            return this;
        }
    }
}