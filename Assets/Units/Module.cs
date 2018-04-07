using UnityEngine;

namespace Assets.Units.Modules
{
    public class Module : MonoBehaviour
    {
        [SerializeField] private float _dragGrounded;
        [SerializeField] protected LayerMask FloorLayerMask;

        public virtual Collider Collider { get; protected set; }

        public float StunResistanceTime = 10;

        [HideInInspector] public Rigidbody Rigidbody { get; private set; }

        protected float StunTimeLeft { get; private set; }

        protected float StunModifier
        {
            get { return Mathf.Max(0, 1 - StunTimeLeft / StunResistanceTime); }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Vector3 Direction
        {
            get { return transform.forward; }
        }

        public bool IsGrounded { get; protected set; }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            IsGrounded = true;
            _dragGrounded = Rigidbody.drag;
        }

        public virtual void Stun(float time)
        {
            StunTimeLeft += time;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            OnCollisionStay(collision);
        }

        protected virtual void OnCollisionStay(Collision collision)
        {
            if (FloorLayerMask.value == (FloorLayerMask.value | (1 << collision.gameObject.layer))) IsGrounded = true;
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            if (FloorLayerMask.value == (FloorLayerMask.value | (1 << collision.gameObject.layer))) IsGrounded = false;
        }

        protected virtual void FixedUpdate()
        {
            StunTimeLeft = Mathf.Max(0, StunTimeLeft - Time.fixedDeltaTime);
            Rigidbody.drag = IsGrounded ? _dragGrounded : 0;
        }
        
        protected virtual void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2, transform.position + 3 * transform.forward);
            }
        }
    }
}