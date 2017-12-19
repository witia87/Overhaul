using Assets.Units.Helpers;
using UnityEngine;

namespace Assets.Units.Modules
{
    public class Module : MonoBehaviour
    {
        [SerializeField] protected LayerMask FloorLayerMask;
        protected CapsuleCollider CapsuleCollider;
        public HeadModule HeadModule;

        public float StunResistanceTime = 10;

        [HideInInspector]
        public Rigidbody Rigidbody { get; private set; }

        protected float StunTimeLeft
        {
            get
            {
                if (HeadModule != null)
                {
                    return HeadModule.StunTimeLeft;
                }
                return StunResistanceTime;
            }
        }

        protected float StunModifier
        {
            get { return Mathf.Max(0, 1 - StunTimeLeft / StunResistanceTime); }
        }

        public bool IsStuned
        {
            get { return StunModifier <= 0; }
        }

        public virtual Vector3 Bottom
        {
            get { return transform.TransformPoint(new Vector3(0, -0.5f, 0)); }
        }

        public virtual Vector3 Top
        {
            get { return transform.TransformPoint(new Vector3(0, 0.5f, 0)); }
        }

        public Vector3 Center
        {
            get { return transform.TransformPoint(new Vector3(0, 0, 0)); }
        }

        public bool IsGrounded { get; protected set; }

        private float _dragGrounded;
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            CapsuleCollider = GetComponent<CapsuleCollider>();
            IsGrounded = true;
            _dragGrounded = Rigidbody.drag;
        }

        public virtual void Stun(float time)
        {
            if (HeadModule != null)
            {
                HeadModule.Stun(time);
            }
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
            }
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            if (FloorLayerMask.value == (FloorLayerMask.value | (1 << collision.gameObject.layer)))
            {
                IsGrounded = false;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2, transform.position + 3 * transform.forward);
            }
        }

        protected virtual void FixedUpdate()
        {
            Rigidbody.drag = IsGrounded ? _dragGrounded : 0;
        }
    }
}