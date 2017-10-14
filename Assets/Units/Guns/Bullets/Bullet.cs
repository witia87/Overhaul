using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Units.Guns.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private float _maximalVelocity = 0.001f;
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
            _maximalVelocity = Mathf.Max(_maximalVelocity, _rigidbody.velocity.magnitude);
            _rigidbody.AddForce(Physics.gravity * (1 - _rigidbody.velocity.magnitude / _maximalVelocity),
                ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (ModuleLayerMask.value == (ModuleLayerMask.value | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.GetComponent<Module>().Stun(StunTime);
            }
        }
    }
}