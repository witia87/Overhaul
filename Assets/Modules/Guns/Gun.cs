using Assets.Modules.Guns.Bullets;
using Assets.Modules.Units.Bodies;
using UnityEngine;

namespace Assets.Modules.Guns
{
    public class Gun : Module, IGun
    {
        private BulletsFactory _bulletsFactory;
        [SerializeField] private float _refreshTime = 0.2f;

        private float _timeLeft;

        protected ConfigurableJoint ConfigurableJoint;

        private void Awake()
        {
            base.Awake();
            Collider = GetComponentInChildren<Collider>();
            _bulletsFactory = GetComponentInChildren<BulletsFactory>();
            ConfigurableJoint = GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            _timeLeft = Mathf.Max(0, _timeLeft - Time.fixedDeltaTime);
        }

        public void Fire()
        {
            if (_timeLeft <= 0)
            {
                _timeLeft = _refreshTime;
                _bulletsFactory.Create();
            }
        }

        protected void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
            }
        }

        public virtual void MountOnto(TorsoModule torso)
        {
            ConfigurableJoint.connectedBody = torso.Rigidbody;
            ConfigurableJoint.connectedAnchor = torso.GunSlotPosition;
        }
    }
}