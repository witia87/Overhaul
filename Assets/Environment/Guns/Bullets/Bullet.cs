using UnityEngine;

namespace Assets.Environment.Guns.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _maximalVelocity;
        private Rigidbody _rigidbody;
        public float BulletMaximalLifetime = 2f;

        public Vector3 InitialPosition { get; set; }

        public float StunTime { get; set; }
        public LayerMask ModuleLayerMask { get; set; }

        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            Destroy(gameObject, BulletMaximalLifetime);
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude > _maximalVelocity.magnitude)
            {
                _maximalVelocity = _rigidbody.velocity;
            }

            _rigidbody.AddForce(
                Physics.gravity * (1 - _rigidbody.velocity.magnitude / _maximalVelocity.magnitude),
                ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (ModuleLayerMask.value == (ModuleLayerMask.value | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.GetComponent<Module>().Hit(collision.impulse.magnitude);
            }
        }
    }
}