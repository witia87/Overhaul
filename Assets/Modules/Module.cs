using UnityEngine;

namespace Assets.Modules
{
    public class Module : Stunnable
    {
        private float _baseDrag;
        [SerializeField] private float _dragGroundedPenalty;
        [SerializeField] protected LayerMask FloorLayerMask;

        [HideInInspector]
        public virtual Collider Collider { get; protected set; }

        [HideInInspector]
        public Rigidbody Rigidbody { get; private set; }

        public bool IsGrounded { get; protected set; }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _baseDrag = Rigidbody.drag;
            Collider = GetComponent<Collider>();
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