using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Modules
{
    public class Unit : MonoBehaviour, IUnitControl
    {
        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Rigidbody Rigidbody { get; private set; }

        private MovementModule _movementModule;
        public IMovementControl Movement { get { return _movementModule; } }
        private TargetingModule _targetingModule;
        public ITargetingControl Targeting { get { return _targetingModule; } }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _movementModule = GetComponentInChildren<MovementModule>();
            _targetingModule = GetComponentInChildren<TargetingModule>();
        }

        private void LateUpdate()
        {
            //_targetingModule.transform.position = transform.position + Vector3.up * Movement.CrouchLevel;
        }

        private void Update()
        {
        }
    }
}