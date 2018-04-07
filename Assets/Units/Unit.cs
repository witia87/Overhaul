using Assets.Units.Bodies.Coordinator;
using Assets.Units.Guns;
using Assets.Units.Modules;
using Assets.Units.Modules.Coordinator.Vision;
using UnityEngine;

namespace Assets.Units
{
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private FractionId _fractionId;
        [SerializeField] private Gun _gun;

        private LegsModule _legsModule;
        private TorsoModule _torsoModule;
        private BodyCoordinator _bodyCoordinator;


        private UnitControl _unitControl = new UnitControl();

        private VisionSensor _visionSensor;

        public IVisionSensor Vision
        {
            get { return _visionSensor; }
        }

        public IUnitControl Control
        {
            get { return _unitControl; }
        }

        public IGun Gun
        {
            get { return _gun; }
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

        private void Awake()
        {
            _legsModule = GetComponentInChildren<LegsModule>();
            _torsoModule = GetComponentInChildren<TorsoModule>();
            _torsoModule.SetupLegs(_legsModule);
            _visionSensor = GetComponentInChildren<VisionSensor>();
            _bodyCoordinator = new BodyCoordinator(_torsoModule, _legsModule, _unitControl);
            Physics.IgnoreCollision(_gun.Collider, _legsModule.Collider);
        }

        private void FixedUpdate()
        {
            _bodyCoordinator.FixedUpdate();
        }

        private void OnGUI()
        {
            _bodyCoordinator.OnGUI();
        }
    }
}