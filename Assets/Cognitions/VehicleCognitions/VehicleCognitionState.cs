using UnityEngine;

namespace Assets.Cognitions.VehicleCognitions
{
    public abstract class VehicleCognitionState : CognitionState<VehicleCognitionStateIds>
    {
        private Vector3 _lastAngle;

        private Vector3 _lastPosition;
        private float _timeStuck;

        protected VehicleCognitionState(Cognition<VehicleCognitionStateIds> parrentCognition,
            VehicleCognitionStateIds id)
            : base(parrentCognition, id)
        {
        }

        protected bool IsStuck()
        {
            var deltaMagnitute = (Core.gameObject.transform.position - _lastPosition).magnitude;
            if (deltaMagnitute < 1)
            {
                _timeStuck += Time.deltaTime;
                if (_timeStuck > 2)
                {
                    _timeStuck = 0;
                    return true;
                }
            }
            else
            {
                _timeStuck = 0;
                _lastPosition = Core.gameObject.transform.position;
                _lastAngle = Core.gameObject.transform.forward;
            }
            return false;
        }
    }
}