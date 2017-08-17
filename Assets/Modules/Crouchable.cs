using UnityEngine;

namespace Assets.Modules
{
    public class CrouchHelper
    {
        private float _crouchTime = 0.2f;
        private bool _isSetToCrouch = false;
        private float _minimalCrouchLevel = 0.5f; // max is always 1
        private float _crouchDownVelocity = 0;
        private float _crouchUpVelocity = 0;

        public float CrouchLevel = 1;

        private MeshCollider _meshCollider;
        private Mesh _initialMesh;
        private Vector3[] _initialVerts;
        private void CrouchHelper()
        {
        }

        private void ScaleMeshVerticaly(MeshCollider collider, float scale)
        {
            var verts = (Vector3[])_initialVerts.Clone();
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].y *= scale;
            }
            collider.sharedMesh.vertices = verts;
            collider.convex = true;
        }

        public void Crouch()
        {
            _isSetToCrouch = true;
        }

        public void StopCrouching()
        {
            _isSetToCrouch = false;
        }

        protected void Update()
        {
            if (_isSetToCrouch)
            {
                if (CrouchLevel > _minimalCrouchLevel)
                {
                    CrouchLevel = Mathf.SmoothDamp(CrouchLevel, _minimalCrouchLevel, ref _crouchDownVelocity,
                        _crouchTime);
                    ScaleMeshVerticaly(_meshCollider, CrouchLevel);

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
                    ScaleMeshVerticaly(_meshCollider, CrouchLevel);

                }
                else
                {
                    _crouchUpVelocity = 0;
                }
            }
        }

    }
}