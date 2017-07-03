using UnityEngine;

namespace Assets.Modules.Turrets.Guns.Bullets
{
    public class BulletComponent : MonoBehaviour, IBulletComponent
    {
        private readonly float _bulletLifetime = 2;
        private readonly float _initialSpeed = 50;
        private readonly float _mass = 0.005f;

        private Rigidbody _rigidbody;

        public Vector3 InitialPosition { get; set; }

        private void Awake()
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.AddRelativeForce(Vector3.forward*_initialSpeed, ForceMode.VelocityChange);
            _rigidbody.mass = _mass;
            gameObject.layer = 2;
            Destroy(gameObject, _bulletLifetime);
        }

        private void Update()
        {
        }
    }
}