using UnityEngine;

namespace Assets.Environment.Units.Bodies
{
    public class BodyModule : Module
    {
        [SerializeField] private readonly float _couchTime = 0.4f;
        [SerializeField] private float _minimalCrouchLevel = 0.6f;

        [SerializeField] private float _turnAngularAcceleration = 20;
        [SerializeField] private float _turnAngularDrag = 20;
        public CapsuleCollider CapsuleCollider;
        public CrouchHelper CrouchHelper;

        protected bool IsSetToCrouch;

        [SerializeField] private float _standingStraightForce = 20;
        [SerializeField] private float _standingAngularDrag = 20;

        public float MovingForce = 10;

        public Vector3 Bottom
        {
            get { return transform.TransformPoint(new Vector3(0, -CrouchHelper.CurrentHeight / 2, 0)); }
        }

        public Vector3 Top
        {
            get { return transform.TransformPoint(new Vector3(0, CrouchHelper.CurrentHeight / 2, 0)); }
        }

        protected override void Awake()
        {
            base.Awake();
            CapsuleCollider = GetComponent<CapsuleCollider>();
            CrouchHelper = new CrouchHelper(_couchTime, _minimalCrouchLevel, CapsuleCollider.height);
        }

        protected virtual void Update()
        {
            CrouchHelper.Update(IsSetToCrouch);
            IsSetToCrouch = false;
            CapsuleCollider.height = CrouchHelper.CurrentHeight;
        }

        public virtual void Crouch()
        {
            IsSetToCrouch = true;
        }

        public void StraightenUp()
        {
            var cross = Vector3.Cross(transform.up, Vector3.up);
            if (Vector3.Angle(transform.up, Vector3.up) > 90)
            {
                cross.Normalize();
            }

            Rigidbody.AddTorque(cross * _standingStraightForce 
                                * Unit.StunModifier, ForceMode.Force);

            AddStraightenAngularDrag();
        }

        private void AddStraightenAngularDrag()
        {
            var localAngularVelocity =
                transform.InverseTransformDirection(Rigidbody.angularVelocity);
            localAngularVelocity.x = localAngularVelocity.x *
                                     (1 - Time.fixedDeltaTime * _standingAngularDrag);
            localAngularVelocity.z = localAngularVelocity.z *
                                     (1 - Time.fixedDeltaTime * _standingAngularDrag);
            Rigidbody.angularVelocity = transform.TransformDirection(localAngularVelocity);
        }

        public void TurnTowards(Vector3 globalDirection)
        {
            var localDirection = transform.InverseTransformDirection(globalDirection);
            var torque = Vector3.Cross(Vector3.forward, transform.InverseTransformDirection(globalDirection));

            if (Vector3.Dot(Vector3.forward, localDirection) >= 0)
            {
                Rigidbody.AddRelativeTorque(torque * _turnAngularAcceleration 
                                                   * Unit.StunModifier);
            }
            else
            {
                Rigidbody.AddRelativeTorque(torque.normalized * _turnAngularAcceleration *
                                            Unit.StunModifier);
            }

            AddTurnAngularDrag();
        }

        private void AddTurnAngularDrag()
        {
            var localAngularVelocity = 
                transform.InverseTransformDirection(Rigidbody.angularVelocity);
            localAngularVelocity.y = localAngularVelocity.y *
                                     (1 - Time.fixedDeltaTime * _turnAngularDrag);
            Rigidbody.angularVelocity = transform.TransformDirection(localAngularVelocity);
        }
    }
}