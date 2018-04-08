using Assets.Modules.Units.Bodies.Coordinator.States.Base;

namespace Assets.Modules.Units.Bodies.Coordinator.States
{
    public class StandingUp : GroundedState
    {
        public StandingUp(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters)
            : base(legs, torso, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            Legs.Crouch();
            Legs.StraightenUp(0.2f);
            Torso.StraightenUp(0.2f);
            return this;
        }
    }
}