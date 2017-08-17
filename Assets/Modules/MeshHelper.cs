using UnityEngine;

namespace Assets.Editor.Modules
{
    public class MeshHelper
    {
        private Mesh _initialMesh;
        private Vector3[] _initialVerts;
        public MeshHelper(Mesh initialMesh)
        {
            _initialVerts = initialMesh.vertices;
            _initialMesh = initialMesh;
        }

        public void ScaleMeshVerticaly(Mesh mesh, float scale)
        {
            var verts = (Vector3[])_initialVerts.Clone();
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].y *= scale;
            }
            mesh.vertices = verts;
            mesh.RecalculateNormals();
        }

        public void ScaleMeshVerticaly(MeshCollider collider, float scale)
        {
            var verts = (Vector3[])_initialVerts.Clone();
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].y *= scale;
            }
            collider.sharedMesh.vertices = verts;
            collider.convex = true;
        }
    }
}