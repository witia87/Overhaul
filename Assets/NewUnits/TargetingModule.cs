using UnityEngine;

namespace Assets.NewUnits
{
    public class TargetingModule : Module, INewTargetingModuleParameters
    {
        public float HorizontalAngularDrag = 10;

        public MovementModule MovementModule;
        public float OtherAngularDrag = 10;
        public float StandingForce = 10;

        protected override void Awake()
        {
            base.Awake();
            Rigidbody.centerOfMass = new Vector3(0, 0.4f, 0);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Rigidbody.angularVelocity =
                new Vector3(
                    Rigidbody.angularVelocity.x,
                    Rigidbody.angularVelocity.y * (1 - Time.fixedDeltaTime * HorizontalAngularDrag),
                    Rigidbody.angularVelocity.z);

            var localVelocity = transform.InverseTransformDirection(Rigidbody.angularVelocity);
            localVelocity = new Vector3(
                localVelocity.x * (1 - Time.fixedDeltaTime * OtherAngularDrag),
                localVelocity.y,
                localVelocity.z * (1 - Time.fixedDeltaTime * OtherAngularDrag));
            Rigidbody.angularVelocity = transform.TransformDirection(localVelocity);

            Rigidbody.AddRelativeTorque(
                Vector3.Cross(MovementModule.transform.InverseTransformDirection(transform.up), Vector3.up) *
                StandingForce, ForceMode.Impulse);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2, transform.position + transform.forward);
            }
        }
    }
}