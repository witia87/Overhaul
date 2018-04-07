using Assets.Units.Modules.Coordinator.States.Base;

namespace Assets.Units.Modules.Coordinator.States
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