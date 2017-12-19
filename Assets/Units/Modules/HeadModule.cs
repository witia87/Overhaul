using System;
using Assets.Units.Guns;
using Assets.Units.Helpers;
using Assets.Units.Modules.Coordinator.States;
using Assets.Units.Modules.Coordinator.States.Base;
using Assets.Units.Modules.Coordinator.Vision;
using Assets.Vision;
using UnityEngine;

namespace Assets.Units.Modules
{
    /// <summary>
    ///     Class responsible for translating cognitive decisions into HeadModule actions.
    ///     It s important that this scripts are executed Before the Cognitive deci
    /// </summary>
    public class HeadModule : MonoBehaviour, IUnitControl
    {
        [SerializeField] private Vector3 _visionPosition;

        public Vector3 VisionPosition
        {
            get { return transform.TransformPoint(_visionPosition); }
        }

        private VisionStore _visionStore;
        public IVisionObserver VisionObserver;

        [SerializeField] private TorsoModule _torsoModule;

        private ConfigurableJoint _torsoConfigurableJoint;
        
        public float StunTimeLeft { get; private set; }

        public IVisionSensor Vision { get; private set; }

        private void Awake()
        {
            _torsoConfigurableJoint = GetComponent<ConfigurableJoint>();

            _unitStatesFactory = new UnitStatesFactory(_legsModule, _torsoModule);
            _currentState = _unitStatesFactory.CreateStanding(Vector3.forward);

            _visionStore = FindObjectOfType<VisionStore>();
            VisionObserver = _visionStore.RegisterUnit(this);

            Vision = _torsoModule.GetComponentInChildren<VisionSensor>();
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
        

        protected void Update()
        {

            StunTimeLeft = Mathf.Max(0, StunTimeLeft - Time.deltaTime);
            _jumpTimeLeft = Mathf.Max(0, _jumpTimeLeft - Time.deltaTime);
            if (_torsoModule != null)
            {
                SetupLegsJoint();
            }
        }

        public void MountTorso(TorsoModule torso)
        {
            if (_torsoModule != null)
            {
                throw new ApplicationException("Legs already mounted");
            }
            _torsoModule = torso;
            _torsoConfigurableJoint.connectedBody = _torsoModule.Rigidbody;
            SetupLegsJoint();
        }

        private void SetupLegsJoint()
        {
            _torsoConfigurableJoint.connectedAnchor = new Vector3(0, _torsoModule.CrouchHelper.CurrentHeight / 2, 0);
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
            GUI.Label(rect, _legsModule.IsGrounded ? "IsGrounded" : "IsAirborn", guiStyle);
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
        }
    }
}