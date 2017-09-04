using System;
using Assets.NewUnits.Helpers;
using UnityEngine;

namespace Assets.NewUnits
{
    public class Module : MonoBehaviour
    {
        private const float Epsilon = 0.0000001f;
        public float Acceleration = 100;
        public float AngularAcceleration = 10;

        protected CrouchHelper CrouchHelper;

        public float CrouchTime = 0.4f;

        protected bool IsStuned;
        public Mesh Mesh;
        public float MinimalCrouchLevel = 0.4f;
        public Rigidbody Rigidbody;

        public Vector3 Size = new Vector3(1, 1, 1);
        protected float StunTimeLeft;

        /// <summary>
        ///     Direction given without the aspect of vertical deviation. It's normalized, and it's .y = 0.
        /// </summary>
        public Vector3 ModuleLogicDirection
        {
            get
            {
                if (Mathf.Abs(transform.forward.y) > 0.99f) // TODO: Verify whether this condition might be removed.
                {
                    throw new ApplicationException(
                        "Module is almost in horisontal position which does not allow it to calculate its logical direction.");
                }
                return NormalizeVector(transform.forward);
            }
        }

        public Vector3 ModuleLogicPosition
        {
            get
            {
                var position = transform.position;
                position.y = 0;
                return position;
            }
        }

        public float CrouchLevel
        {
            get { return CrouchHelper.CrouchLevel; }
        }

        public Vector3 Bottom
        {
            get { return transform.TransformPoint(new Vector3(0, -Size.y / 2, 0) * CrouchLevel); }
        }

        public Vector3 Top
        {
            get { return transform.TransformPoint(new Vector3(0, Size.y / 2, 0) * CrouchLevel); }
        }

        public Vector3 Center
        {
            get { return transform.TransformPoint(new Vector3(0, 0, 0)); }
        }

        public Vector3 ModuleUp { get { return transform.up; } }
        public Vector3 ModuleForward { get { return transform.forward; } }
        public Vector3 ModuleRigth { get { return transform.right; } }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            CrouchHelper = new CrouchHelper(GetComponent<CapsuleCollider>(), CrouchTime, MinimalCrouchLevel);
        }

        protected virtual void FixedUpdate()
        {
            CrouchHelper.FixedUpdate();
        }

        protected virtual void Update()
        {
            StunTimeLeft = Mathf.Max(0, StunTimeLeft - Time.deltaTime);
        }

        protected Vector3 NormalizeVector(Vector3 v)
        {
            v.y = 0;
            return v.normalized;
        }

        /// <summary>
        ///     Method allows to add a torue around vertical axis of the module
        /// </summary>
        /// <param name="value">Must be from range (rotate left) [-1..1] (rotate right)</param>
        public void AddRotation(float value)
        {
            if (Mathf.Abs(value) > 1 + Epsilon)
            {
                throw new ApplicationException("Value of the torque must be from [-1..1] range.");
            }
            Rigidbody.AddRelativeTorque(Vector3.up * value * AngularAcceleration, ForceMode.Force);
        }

        public void SetCrouch(bool isSetToCrouch)
        {
            CrouchHelper.SetCrouch(isSetToCrouch);
        }

        public virtual void Stun(float time)
        {
            StunTimeLeft += time;
        }
    }
}