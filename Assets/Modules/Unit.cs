using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Modules
{
    public class Unit : MonoBehaviour, IUnitControl
    {
        public Rigidbody Rigidbody { get; private set; }

        public IMovementControl Movement { get; private set; }
        public ITargetingControl Targeting { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Movement = GetComponentInChildren<MovementModule>();
            Targeting = GetComponentInChildren<TargetingModule>();
        }

        private void Update()
        {
        }
    }
}