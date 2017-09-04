using System;
using Assets.NewUnits.Helpers;
using Assets.NewUnits.States;
using UnityEngine;

namespace Assets.NewUnits
{
    public class Unit : MonoBehaviour
    {
        private readonly AngleCalculator _angleCalculator = new AngleCalculator();
        private readonly UnitControlParameters _unitControlParameters = new UnitControlParameters();

        private UnitState _currentState;
        private MovementModule _movementModule;
        private TargetingModule _targetingModule;
        private UnitStates _unitStates;

        private void Awake()
        {
            _movementModule = GetComponentInChildren<MovementModule>();
            _targetingModule = GetComponentInChildren<TargetingModule>();
            _unitStates = new UnitStates(_movementModule, _targetingModule, _unitControlParameters);

            _currentState = _unitStates.Standing;
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(_movementModule.transform.forward.y) > 0.99f ||
                Mathf.Abs(_targetingModule.transform.forward.y) > 0.99f)
            {
                return;
            } // Take controll away when module is horizontal
            if (!_unitControlParameters.IsMoveGlobalDirectionSet)
            {
                _currentState = _unitStates.Standing;
            }
            else
            {
                var parametersAngle =
                    _angleCalculator.CalculateLogicAngle(_unitControlParameters.MoveGlobalDirection,
                        _unitControlParameters.LookGlobalDirection);
                if (Mathf.Abs(parametersAngle) <= 120)
                {
                    _currentState = _unitStates.MovingForward;
                }
                else
                {
                    _currentState = _unitStates.MovingBackward;
                }
            }

            _currentState.FixedUpdate();
        }

        private void Update()
        {
        }

        private void OnGUI()
        {
            _currentState.OnGUI();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                _currentState.OnDrawGizmos();
            }
        }

        /// <summary>
        ///     Makes unit perform actions in order to look in the desired direction.
        /// </summary>
        /// <param name="globalDirection">Vector3 needs to be normalized and has y=0</param>
        public void LookTowards(Vector3 globalDirection)
        {
            _unitControlParameters.IsLookGlobalDirectionSet = true;
            _unitControlParameters.LookGlobalDirection = globalDirection;
        }

        public void LookAt(Vector3 point)
        {
            if (Mathf.Abs(point.y) > 0.00000001f)
            {
                throw new ApplicationException("Given point to look does not have its .y = 0.");
            }
            LookTowards((point - _targetingModule.ModuleLogicPosition).normalized);
        }

        public void Move(Vector3 globalDirection, float speedModifier)
        {
            _angleCalculator.CheckIfVectorIsLogic(globalDirection);
            _unitControlParameters.IsMoveGlobalDirectionSet = true;
            _unitControlParameters.MoveGlobalDirection = globalDirection;
        }

        public void StopMoving()
        {
            _unitControlParameters.IsMoveGlobalDirectionSet = false;
        }

        public void SetCrouch(bool isSetToCrouch)
        {
            _movementModule.SetCrouch(isSetToCrouch);
            _targetingModule.SetCrouch(isSetToCrouch);
        }

        public void Jump(Vector3 globalLogicDirection, float jumpForceModifier)
        {
            _angleCalculator.CheckIfVectorIsLogic(globalLogicDirection);
            _movementModule.Jump(globalLogicDirection, jumpForceModifier);
        }
    }
}