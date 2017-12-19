using Assets.Units.Modules.Coordinator.States.Base;
using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class Crawling : HorizontalState
    {
        public Crawling(LegsModule legs, TorsoModule torso, UnitStatesFactory statesFactory,
            Vector3 initialGlobalLookDirection) : base(legs, torso, statesFactory, initialGlobalLookDirection)
        {
            GlobalLookDirection = initialGlobalLookDirection;
        }

        protected Vector3 GetLogicVector(Vector3 vector)
        {
            vector.z = 0;
            return vector.normalized;
        }

        protected Vector3 MoveLogicDirection;
        protected float SpeedModifier;
        public override UnitState Move(Vector3 moveLogicDirection, float speedModifier)
        {
            var torsoLogicDirection = GetLogicVector(Torso.transform.up);
            if (Vector3.Angle(torsoLogicDirection, GlobalLookDirection) > 135)
            {
                return StatesFactory.CreateLayingBack(GlobalLookDirection);
            }
            MoveLogicDirection = moveLogicDirection;
            SpeedModifier = speedModifier;
            return this;
        }

        public override UnitState StopMoving()
        {
            return StatesFactory.CreateStandingUp(GlobalLookDirection);
        }

        public override UnitState VerifyPhysicConditions()
        {
            if (Legs.IsStanding)
            {
                return StatesFactory.CreateStanding(GlobalLookDirection);
            }
            if (!Legs.IsGrounded && !Torso.IsGrounded)
            {
                return StatesFactory.CreateGliding(GlobalLookDirection);
            }
            return this;
        }

        public override UnitState FixedUpdate()
        {
            Torso.TurnTowards((Vector3.down + GlobalLookDirection).normalized);
            Torso.FlipTowards((Vector3.down + GlobalLookDirection/2).normalized);
            Torso.Rigidbody.AddForce(-Physics.gravity/2, ForceMode.Acceleration);
            Legs.TurnTowards(Vector3.down);
            Legs.FlipTowards(Vector3.down);

            Torso.AlignWith(GlobalLookDirection);
            Legs.AlignWith(Torso.transform.up);

            Torso.Crawl(MoveLogicDirection);
            Legs.Crawl(MoveLogicDirection);
            return this;
        }
    }
}