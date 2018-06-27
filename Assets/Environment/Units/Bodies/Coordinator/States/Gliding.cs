using Assets.Environment.Units.Bodies.Coordinator.States.Base;

namespace Assets.Environment.Units.Bodies.Coordinator.States
{
    public class Gliding : UnitState
    {
        public Gliding(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
            : base(torso, legs, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards(Parameters.LookLogicDirection);
            //Torso.StraightenUp(Mathf.Abs(Vector3.Dot(Torso.transform.up, Vector3.up) * 0.4f));
            //Legs.(Mathf.Abs(Vector3.Dot(Legs.transform.up, Vector3.up) * 0.2f));

            return this;
        }
    }
}