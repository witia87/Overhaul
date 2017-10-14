using UnityEngine;

namespace Assets.Units.Helpers
{
    public class CrouchHelper
    {
        private readonly float _crouchTime;
        private float _crouchDownVelocity;
        private float _crouchUpVelocity;

        private float _stunTimeLeft;

        public CrouchHelper(float crouchTime)
        {
            _crouchTime = crouchTime;
            CrouchLevel = 1;
        }

        public float CrouchLevel { get; private set; }

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
                if (CrouchLevel > 0)
                {
                    CrouchLevel = Mathf.SmoothDamp(CrouchLevel, minTarget, ref _crouchDownVelocity,
                        _crouchTime);
                }
                else
                {
                    _crouchDownVelocity = 0;
                }
            }
            else
            {
                if (CrouchLevel < 1)
                {
                    CrouchLevel = Mathf.SmoothDamp(CrouchLevel, 1, ref _crouchUpVelocity,
                        _crouchTime);
                }
                else
                {
                    _crouchUpVelocity = 0;
                }
            }
        }
    }
}