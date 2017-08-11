using UnityEngine;

namespace Assets.Modules.Targeting.Guns.Bullets
{
    public class Bullet : MonoBehaviour, IBulletComponent
    {
        private float _maximalVelocity = 0.001f;
        private Rigidbody _rigidbody;
        public float BulletMaximalLifetime = 2f;

        public Vector3 InitialPosition { get; set; }

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
    }
}