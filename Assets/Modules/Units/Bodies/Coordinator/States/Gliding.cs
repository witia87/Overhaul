using Assets.Modules.Units.Bodies.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Modules.Units.Bodies.Coordinator.States
{
    public class Gliding : UnitState
    {
        public Gliding(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters) : base(torso,
            legs, parameters)
        {
        }

        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards(Parameters.LookLogicDirection);
            Torso.StraightenUp(Mathf.Abs(Vector3.Dot(Torso.transform.up, Vector3.up) * 0.4f));
            Legs.StraightenUp(Mathf.Abs(Vector3.Dot(Legs.transform.up, Vector3.up) * 0.2f));

            return this;
        }
    }
}