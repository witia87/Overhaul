/*using System;
using Assets.Units.Guns;
using Assets.Units.Helpers;
using Assets.Units.Modules;
using Assets.Units.Modules.States;
using Assets.Units.Modules.States.Base;
using Assets.Units.Vision;
using UnityEngine;

namespace Assets.Units
{
    /// <summary>
    ///     Class responsible for translating cognitive decisions into Unit actions.
    ///     It s important that this scripts are executed Before the Cognitive deci
    /// </summary>
    public class Unit : MonoBehaviour, IUnitControl
    {
        private readonly AngleCalculator _angleCalculator = new AngleCalculator();

        private readonly float CrouchTime = 0.4f;
        private CrouchHelper _crouchHelper;
        private UnitState _currentState;
        private bool _isSetToCrouch;

        public MovementModule MovementModule { get; private set; }
        public TargetingModule TargetingModule { get; private set; }

        private UnitStatesFactory _unitStatesFactory;

        private bool _wasMoveRequestedThisTurn;
        private bool _wasSetToCrouchThisTurn;

        public float StunTimeLeft { get; private set; }
        public IGunControl Gun { get; private set; }

        /// <summary>
        ///     Makes unit perform actions in order to look in the desired direction.
        /// </summary>
        /// <param name="globalDirection">Vector3 needs to be normalized and has y=0</param>
        public void LookTowards(Vector3 globalDirection)
        {
            throw new NotImplementedException("LookTowards is not implemeted in Unit.");
        }


        public void LookAt(Vector3 globalPoint)
        {
            var newLogicLookDirection = globalPoint - TargetingModule.Center;
            newLogicLookDirection.y = 0;
            _currentState = _currentState.LookTowards(newLogicLookDirection.normalized);
        }

        public void Move(Vector3 logicDirection, float speedModifier)
        {
            _wasMoveRequestedThisTurn = true;
            _angleCalculator.CheckIfVectorIsLogic(logicDirection);
            _currentState = _currentState.Move(logicDirection, speedModifier);
        }

        public void Crouch()
        {
            _wasSetToCrouchThisTurn = true;
        }

        public void Jump(Vector3 globalDirection, float jumpForceModifier)
        {
            _currentState = _currentState.Jump(globalDirection, jumpForceModifier);
        }

        public Vector3 Position
        {
            get { return TargetingModule.ModuleLogicPosition; }
        }

        public Vector3 Center
        {
            get { return TargetingModule.Center; }
        }

        public Vector3 Velocity
        {
            get { return TargetingModule.Rigidbody.velocity; }
        }

        public IVisionSensor Vision { get; private set; }

        private void Awake()
        {
            TargetingModule = GetComponent<TargetingModule>();
            MovementModule = GetComponentInChildren<MovementModule>();
            Gun = GetComponentInChildren<Gun>();

            _unitStatesFactory = new UnitStatesFactory(MovementModule, TargetingModule);
            _currentState = _unitStatesFactory.CreateStanding(Vector3.forward);

            _crouchHelper = new CrouchHelper(CrouchTime);
            Vision = TargetingModule.GetComponentInChildren<VisionSensor>();
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

        private void Update()
        {
            StunTimeLeft = Mathf.Max(0, StunTimeLeft - Time.deltaTime);
            _crouchHelper.Update(_isSetToCrouch);
            TargetingModule.SetCrouch(_crouchHelper.CrouchLevel);
            MovementModule.SetCrouch(_crouchHelper.CrouchLevel);
        }

        /// <summary>
        ///     In LateUpdate the capitalisation of decisions is performed, so it will be executed by ModulesDirectior in the
        ///     FixedUpdates that follow.
        /// </summary>
        private void LateUpdate()
        {
            if (!_wasMoveRequestedThisTurn)
            {
                _currentState = _currentState.StopMoving();
            }
            _wasMoveRequestedThisTurn = false;

            _isSetToCrouch = _wasSetToCrouchThisTurn;
            _wasSetToCrouchThisTurn = false;
        }

        private void OnGUI()
        {
            _currentState.OnGUI();
            int w = Screen.width, h = Screen.height;

            var guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.fontSize = h * 2 / 100;
            guiStyle.normal.textColor = new Color(1.0f, 0.0f, 0.5f, 1.0f);
            var rect = new Rect(w - 200, h * 2 / 100, w, 2 * h * 2 / 100);
            GUI.Label(rect, MovementModule.IsGrounded ? "IsGrounded" : "IsAirborn", guiStyle);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                _currentState.OnDrawGizmos();
            }
        }

        public void Stun(float stunTime)
        {
            StunTimeLeft += stunTime;
            _crouchHelper.Stun(stunTime);
        }
    }
}*/