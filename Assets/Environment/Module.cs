using Assets.Environment.Floors;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Environment
{
    public class Module : Localizable
    {
        private float _baseDrag;
        [SerializeField] private float _dragGroundedPenalty = 10;
        protected LayerMask FloorLayerMask;
        protected Unit Unit;

        [HideInInspector]
        public virtual Collider Collider { get; protected set; }

        [HideInInspector]
        public Rigidbody Rigidbody { get; private set; }

        public bool IsGrounded { get; protected set; }

        protected virtual void Awake()
        {
            Unit = transform.root.GetComponent<Unit>();
            Rigidbody = GetComponent<Rigidbody>();
            _baseDrag = Rigidbody.drag;
            Collider = GetComponent<Collider>();
            FloorLayerMask = FindObjectOfType<Floor>().FloorLayerMask;
        }

        public virtual void Hit(float impulseStrength)
        {
            Unit.Stun(impulseStrength);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            OnCollisionStay(collision);
        }

        protected virtual void OnCollisionStay(Collision collision)
        {
            if (FloorLayerMask.value == (FloorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = true;
                RefreshDrag();
            }
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            if (FloorLayerMask.value == (FloorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = false;
                RefreshDrag();
            }
        }

        private void RefreshDrag()
        {
            Rigidbody.drag = IsGrounded ? _baseDrag + _dragGroundedPenalty : _baseDrag;
        }
    }
}