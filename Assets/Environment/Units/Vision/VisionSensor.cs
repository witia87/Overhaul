using UnityEngine;

namespace Assets.Environment.Units.Vision
{
    public class VisionSensor : Localizable, IVisionSensor
    {
        [SerializeField] private float _sightAngle = 120;

        [SerializeField] private float _sightLength = 10;
        public Unit Unit { get; private set; }

        public float SightLength
        {
            get { return _sightLength; }
        }

        public float SightAngle
        {
            get { return _sightAngle * Unit.StunModifier; }
        }

        private void Awake()
        {
            Unit = transform.root.GetComponent<Unit>();
        }
    }
}