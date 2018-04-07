using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class Gliding : UnitState
    {
        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards(Parameters.LookLogicDirection);
            Torso.StraightenUp(Mathf.Abs(Vector3.Dot(Torso.transform.up, Vector3.up) * 0.4f));
            Legs.StraightenUp(Mathf.Abs(Vector3.Dot(Legs.transform.up, Vector3.up) * 0.2f));
            
            return this;
        }

        public Gliding(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters) : base(torso, legs, parameters)
        {
        }
    }
}