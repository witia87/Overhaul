using Assets.Environment.Guns;
using Assets.Environment.Units.Bodies;
using Assets.Environment.Units.Bodies.Coordinator;
using Assets.Environment.Units.Vision;
using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Environment.Units
{
    public class Unit : MonoBehaviour, IUnit
    {
        private BodyCoordinator _bodyCoordinator;
        [SerializeField] private FractionId _fractionId;
        [SerializeField] private Gun _gun;
        [SerializeField] private int _unitScale;

        private LegsModule _legsModule;

        private float _stunTimeLeft;
        private TorsoModule _torsoModule;

        private UnitControl _unitControl;

        public float StunResistanceTime = 10;

        public float StunModifier
        {
            get { return Mathf.Max(0, 1 - _stunTimeLeft / StunResistanceTime); }
        }

        public bool IsStunned
        {
            get { return StunModifier < 0.2f; }
        }

        public IUnitControl Control
        {
            get { return _unitControl; }
        }

        public IGun Gun
        {
            get { return _gun; }
        }

        public IVisionSensor Vision
        {
            get { return _torsoModule.VisionSensor; }
        }

        public Vector3 Position
        {
            get { return _torsoModule.Position; }
        }

        public Vector3 LogicPosition
        {
            get
            {
                var position = _torsoModule.Position;
                position.y = 0;
                return position;
            }
        }

        public Vector3 Velocity
        {
            get { return _torsoModule.Rigidbody.velocity; }
        }

        public FractionId Fraction
        {
            get { return _fractionId; }
        }

        public int UnitScale
        {
            get
            {
                return _unitScale;
            }
        }

        private void Awake()
        {
            _legsModule = GetComponentInChildren<LegsModule>();
            _torsoModule = GetComponentInChildren<TorsoModule>();
            _torsoModule.SetupLegs(_legsModule);
            _unitControl = new UnitControl(this);
            _bodyCoordinator = new BodyCoordinator(this, _torsoModule, _legsModule, _unitControl);
            Physics.IgnoreCollision(_gun.Collider, _legsModule.Collider);
        }

        private void Update()
        {
            _unitControl.Update();
        }

        private void FixedUpdate()
        {
            _stunTimeLeft = Mathf.Max(0, _stunTimeLeft - Time.fixedDeltaTime);
            _unitControl.FixedUpdate();
            _bodyCoordinator.FixedUpdate();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying && DebugStore.IsDebugMode)
            {
                _bodyCoordinator.OnDrawGizmos();
            }
        }

        public virtual void Stun(float time)
        {
            _stunTimeLeft += time;
        }
    }
}