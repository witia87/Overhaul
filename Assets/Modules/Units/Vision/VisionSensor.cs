using UnityEngine;

namespace Assets.Modules.Units.Vision
{
    public class VisionSensor : Stunnable, IVisionSensor
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
            get { return _sightAngle * StunModifier; }
        }

        private void Awake()
        {
            Unit = transform.root.GetComponent<Unit>();
        }
    }
}