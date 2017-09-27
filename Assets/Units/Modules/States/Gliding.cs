using Assets.Units.Modules.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.States
{
    public class Gliding : AirbornState
    {
        private float _alignForce;
        private float _flipModifier;

        private bool _wasFlipSet;

        public Gliding(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory,
            Vector3 globalLookDirection, Vector3 flightLogicDirection) :
            base(movement, targeting, statesFactory, globalLookDirection, flightLogicDirection)
        {
        }

        public override UnitState Move(Vector3 moveLogicDirection, float speedModifier)
        {
            if (Vector3.Angle(moveLogicDirection, FlightLogicDirection) < 45)
            {
                _flipModifier = -speedModifier;
                _wasFlipSet = true;
            }
            else
            {
                _wasFlipSet = false;
            }
            return this;
        }

        public override UnitState StopMoving()
        {
            _wasFlipSet = false;
            return this;
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (Movement.IsStanding)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            if ((Movement.IsGrounded || Targeting.IsGrounded) && !_wasFlipSet)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            Targeting.LookTowards(Targeting.transform.InverseTransformDirection(GlobalLookDirection));
            var movementDirection = Movement.transform.InverseTransformDirection(Targeting.transform.forward);
            movementDirection.z = 0;
            Movement.LookTowards(movementDirection);

            if (_wasFlipSet)
            {
                if (!Targeting.IsGrounded && !Movement.IsGrounded)
                {
                    Targeting.AlignTowards(Targeting.transform.InverseTransformDirection(FlightLogicDirection));
                    Movement.AlignTowards(Movement.transform.InverseTransformDirection(FlightLogicDirection));
                    Targeting.AddFlippingForce(FlightLogicDirection);
                }
                Targeting.AddJumpStandingForce();            
            } else
            {
                Movement.PerformStandingStraight();
            }
            return this;
        }
    }
}