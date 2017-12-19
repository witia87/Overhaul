using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class Gliding : UnitState
    {
        public Gliding(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection) : base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
            GlobalLookDirection = initialGlobalLookDirection;
        }
        
        public override UnitState VerifyPhysicConditions()
        {
            if (Legs.IsStanding)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            if (Legs.IsGrounded || Torso.IsGrounded)
            {
                return StatesFactory.CreateLayingBack(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards(GlobalLookDirection);
            Torso.StraightenUp(Mathf.Abs(Vector3.Dot(Torso.transform.up, Vector3.up) * 0.4f));
            Legs.StraightenUp(Mathf.Abs(Vector3.Dot(Legs.transform.up, Vector3.up) * 0.2f));
            
            return this;
        }
    }
}