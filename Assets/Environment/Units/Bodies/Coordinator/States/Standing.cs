using Assets.Environment.Units.Bodies.Coordinator.States.Base;

namespace Assets.Environment.Units.Bodies.Coordinator.States
{
    public class Standing : UnitState
    {
        public Standing(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
            : base(torso, legs, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            var logicLookDirection = GetLogicVector(Parameters.LookLogicDirection);
            Torso.TurnTowards(logicLookDirection);
            Torso.AimAt(Parameters.AimAtDirection);

            Legs.TurnTowards(logicLookDirection);

            Legs.StraightenUp();
            Torso.StraightenUp();
            return this;
        }
    }
}