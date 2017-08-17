using UnityEngine;

namespace Assets.Modules
{
    public class CrouchHelper
    {
        private readonly float _crouchTime;
        private readonly Vector3[] _initialVerts;

        private readonly MeshCollider _meshCollider;
        private readonly float _minimalCrouchLevel; // max is always 1
        private float _crouchDownVelocity;
        private float _crouchUpVelocity;
        private bool _isSetToCrouch;

        public float CrouchLevel = 1;

        public CrouchHelper(MeshCollider meshCollider, float crouchTime, float minimalCrouchLevel)
        {
            _meshCollider = meshCollider;
            _crouchTime = crouchTime;
            _minimalCrouchLevel = minimalCrouchLevel;
            _initialVerts = _meshCollider.sharedMesh.vertices;
        }

        private void ScaleMeshVerticaly(float scale)
        {
            var verts = (Vector3[]) _initialVerts.Clone();
            for (var i = 0; i < verts.Length; i++)
            {
                verts[i].y *= scale;
            }
            _meshCollider.sharedMesh.vertices = verts;
            _meshCollider.convex = true;
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
                    ScaleMeshVerticaly(CrouchLevel);
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
                    ScaleMeshVerticaly(CrouchLevel);
                }
                else
                {
                    _crouchUpVelocity = 0;
                }
            }
        }
    }
}