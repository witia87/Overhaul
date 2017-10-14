using Assets.Units.Guns;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class TargetingModule : Module
    {
        public float FlippingForce = 3;
        protected Gun Gun;

        public Vector3 GunSlotPosition = new Vector3(0.2f, -0.2f, 0.2f);
        public Vector3 GunSlotPositionCrouched = new Vector3(0.2f, 0.1f, 0.2f);

        public float JumpStraighteningForce = 5;

        protected override void Awake()
        {
            base.Awake();
            Gun = GetComponentInChildren<Gun>();
            Rigidbody.centerOfMass = new Vector3(0, 0.4f, 0);
            IsGrounded = false;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                //Gizmos.DrawLine(transform.position + ModuleLogicDirection / 2,
                //    transform.position + ModuleLogicDirection * 15);
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
            }
        }

        public void AddJumpStandingForce()
        {
            Rigidbody.AddTorque(Vector3.Cross(transform.up, Vector3.up) * JumpStraighteningForce * StunModifier,
                ForceMode.Force);
            Rigidbody.AddForce(-Physics.gravity / 8 * StunModifier);
        }

        public void AddFlippingForce(Vector3 logicDirectionOfTheFlip)
        {
            Rigidbody.AddTorque(AngleCalculator.RotateLogicVector(logicDirectionOfTheFlip, 90) * FlippingForce);
        }

        public override void SetCrouch(float crouchLevel)
        {
            base.SetCrouch(crouchLevel);
            Gun.SetSlotPosition(crouchLevel * GunSlotPosition + (1 - crouchLevel) * GunSlotPositionCrouched);
        }
    }
}