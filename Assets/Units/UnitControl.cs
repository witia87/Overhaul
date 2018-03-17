using System;
using Assets.Units.Guns;
using Assets.Units.Helpers;
using Assets.Units.Modules;
using Assets.Units.Modules.Coordinator.States;
using Assets.Units.Modules.Coordinator.States.Base;
using Assets.Units.Modules.Coordinator.Vision;
using UnityEngine;

namespace Assets.Units
{
    public class UnitControl: MonoBehaviour, IUnitControl
    {
        private Gun _gun;
        private TorsoModule _torsoModule;
        private LegsModule _legsModule;

        private UnitState _currentState;
        private UnitStatesFactory _unitStatesFactory;
        
        public FractionId FractionId;
        public float JumpVelocity = 1;
        public float TestFlippingForce = 5;
        private float _jumpRefreshTime = 1;
        private float _jumpTimeLeft;


        public IVisionSensor Vision { get; private set; }

        public IGunControl Gun
        {
            get { return _gun; }
        }

        public Vector3 LogicPosition
        {
            get { return new Vector3(transform.position.x, 0, transform.position.z); }
        }

        public Vector3 Center
        {
            get { return _torsoModule.Center; }
        }

        public Vector3 Velocity
        {
            get { return _torsoModule.Rigidbody.velocity; }
        }

        private void Awake()
        {
            _torsoModule = GetComponent<TorsoModule>();
            _legsModule = GetComponentInChildren<LegsModule>();

            _unitStatesFactory = new UnitStatesFactory(_legsModule, _torsoModule);
            _currentState = _unitStatesFactory.CreateStanding(Vector3.forward);
        }

        private void FixedUpdate()
        {
            var newState = _currentState.VerifyPhysicConditions();
            if (_currentState == newState)
            {
                _currentState = _currentState.FixedUpdate();
            }
            else
            {
                _currentState = newState;
            }
        }


        /// <summary>
        ///     Makes UnitControl perform actions in order to look in the desired direction.
        /// </summary>
        /// <param name="globalDirection">Vector3 needs to be normalized and has y=0</param>
        public void LookTowards(Vector3 globalDirection)
        {
            globalDirection.y = 0;
            _currentState = _currentState.LookTowards(globalDirection.normalized);
        }


        public void LookAt(Vector3 globalPoint)
        {
            var newLogicLookDirection = globalPoint - _torsoModule.Center;
            newLogicLookDirection.y = 0;
            _currentState = _currentState.LookTowards(newLogicLookDirection.normalized);
        }

        public void Move(Vector3 logicDirection, float speedModifier)
        {
            AngleCalculator.CheckIfVectorIsLogic(logicDirection);
            _currentState = _currentState.Move(logicDirection, speedModifier);
        }

        public void Crouch()
        {
            _torsoModule.Crouch();
            _legsModule.Crouch();
        }

        public void Jump()
        {
            throw new NotImplementedException("Jump not implemented");
        }
    }
}