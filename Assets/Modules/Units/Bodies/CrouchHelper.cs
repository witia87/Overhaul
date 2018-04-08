using UnityEngine;

namespace Assets.Modules.Units.Bodies
{
    public class CrouchHelper
    {
        private float _crouchDownVelocity;
        private readonly float _crouchTime;
        private float _crouchUpVelocity;

        private float _currentCrouchLevel;

        private float _initialColliderHeight;
        private float _minimalCrouchLevel;

        private float _stunTimeLeft;

        public CrouchHelper(float crouchTime, float minimalCrouchLevel, float initialColliderHeight)
        {
            _crouchTime = crouchTime;
            _minimalCrouchLevel = minimalCrouchLevel;
            _initialColliderHeight = initialColliderHeight;
            _currentCrouchLevel = 1;
        }


        public float CurrentHeight
        {
            get { return _initialColliderHeight * CrouchModifier; }
        }

        protected float CrouchModifier
        {
            get { return _minimalCrouchLevel + _currentCrouchLevel * (1 - _minimalCrouchLevel); }
        }

        public void Stun(float stunTime)
        {
            _stunTimeLeft += stunTime;
        }

        public void Update(bool isSetToCrouch)
        {
            _stunTimeLeft = Mathf.Max(0, _stunTimeLeft - Time.deltaTime);
            float minTarget = 0;
            if (_stunTimeLeft > 0)
            {
                minTarget = 0.4f;
            }

            if (isSetToCrouch)
            {
                _currentCrouchLevel = Mathf.SmoothDamp(_currentCrouchLevel, minTarget,
                    ref _crouchDownVelocity,
                    _crouchTime);
            }
            else
            {
                _currentCrouchLevel = Mathf.SmoothDamp(_currentCrouchLevel, 1, ref _crouchUpVelocity,
                    _crouchTime);
            }
        }
    }
}