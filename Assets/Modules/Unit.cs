using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Modules
{
    public class Unit : MonoBehaviour, IUnitControl
    {
        private MovementModule _movementModule;
        private TargetingModule _targetingModule;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Rigidbody Rigidbody { get; private set; }

        public IMovementControl Movement
        {
            get { return _movementModule; }
        }

        public ITargetingControl Targeting
        {
            get { return _targetingModule; }
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _movementModule = GetComponentInChildren<MovementModule>();
            _targetingModule = GetComponentInChildren<TargetingModule>();
        }

        private void Update()
        {
            transform.eulerAngles =
                new Vector3(0, transform.eulerAngles.y, 0); // Fixes the minor errors (reason unknown...)
            _targetingModule.transform.position = transform.position + Vector3.up * Movement.CrouchLevel;
        }
    }
}