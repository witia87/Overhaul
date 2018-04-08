using Assets.Modules.Guns;
using Assets.Modules.Units.Bodies;
using Assets.Modules.Units.Bodies.Coordinator;
using Assets.Modules.Units.Vision;
using UnityEngine;

namespace Assets.Modules.Units
{
    public class Unit : MonoBehaviour, IUnit
    {
        private BodyCoordinator _bodyCoordinator;
        [SerializeField] private FractionId _fractionId;
        [SerializeField] private Gun _gun;

        private LegsModule _legsModule;
        private TorsoModule _torsoModule;

        private UnitControl _unitControl = new UnitControl();

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

        private void Awake()
        {
            _legsModule = GetComponentInChildren<LegsModule>();
            _torsoModule = GetComponentInChildren<TorsoModule>();
            _torsoModule.SetupLegs(_legsModule);
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