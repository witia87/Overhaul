using UnityEngine;

namespace Assets.NewUnits.Helpers
{
    public class CrouchHelper
    {
        private readonly CapsuleCollider _capsuleCollider;
        private readonly float _crouchTime;
        private readonly Vector3[] _initialVerts;
        private readonly float _minimalCrouchLevel; // max is always 1
        private float _crouchDownVelocity;
        private float _crouchUpVelocity;
        private bool _isSetToCrouch;

        public float CrouchLevel = 1;

        public CrouchHelper(CapsuleCollider capsuleCollider, float crouchTime, float minimalCrouchLevel)
        {
            _capsuleCollider = capsuleCollider;
            _crouchTime = crouchTime;
            _minimalCrouchLevel = minimalCrouchLevel;
        }


        public void SetCrouch(bool isSetToCrouch)
        {
            _isSetToCrouch = isSetToCrouch;
        }

        public void FixedUpdate()
        {
            if (_isSetToCrouch)
            {
                if (CrouchLevel > _minimalCrouchLevel)
                {
                    CrouchLevel = Mathf.SmoothDamp(CrouchLevel, _minimalCrouchLevel, ref _crouchDownVelocity,
                        _crouchTime);
                    //ScaleMeshVerticaly(CrouchLevel);
                    _capsuleCollider.height = CrouchLevel;
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
                    //ScaleMeshVerticaly(CrouchLevel);
                    _capsuleCollider.height = CrouchLevel;
                }
                else
                {
                    _crouchUpVelocity = 0;
                }
            }
        }
    }
}