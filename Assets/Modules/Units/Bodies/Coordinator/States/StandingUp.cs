using Assets.Modules.Units.Bodies.Coordinator.States.Base;

namespace Assets.Modules.Units.Bodies.Coordinator.States
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
            Legs.StraightenUp(0.2f);
            Torso.StraightenUp(0.2f);
            return this;
        }
    }
}