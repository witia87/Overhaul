using System.Collections.Generic;
using Assets.Sight;
using UnityEngine;

namespace Assets.Units.Vision
{
    public class SightPresenter : MonoBehaviour
    {
        private MeshFilter _meshFilter;

        private SightStore _sightStore;

        [SerializeField] private readonly float _visionHeight = 3;

        public Unit Unit;

        public void Awake()
        {
            _sightStore = FindObjectOfType<SightStore>();

            _meshFilter = GetComponent<MeshFilter>();

            var mesh = new Mesh();

            _meshFilter.mesh = mesh;
        }

        private void Update()
        {
            var polygon = _sightStore.GetSightPolygon(Unit.Position);
            var vertices = GetExtendedVertices(_sightStore.Center, polygon);
            _meshFilter.mesh.Clear();
            _meshFilter.mesh.vertices = vertices;
            _meshFilter.mesh.triangles = GenerateExtendedTriangulation(polygon.Count, vertices);
        }

        private Vector3[] GetExtendedVertices(Vector2 center, List<Vector2> polygon)
        {
            var output = new Vector3[polygon.Count + 1 + polygon.Count];
            output[0] = new Vector3(center.x, 2, center.y);

            for (var i = 0; i < polygon.Count; i++)
            {
                output[i + 1] = new Vector3(polygon[i].x, _visionHeight, polygon[i].y);
                output[1 + polygon.Count + i] = new Vector3(polygon[i].x, 0, polygon[i].y);
            }
            return output;
        }

        private int[] GenerateExtendedTriangulation(int n, Vector3[] vertices)
        {
            var output = new int[9 * n];
            for (var i = 0; i < n; i++)
            {
                output[3 * i] = 0;
                output[3 * i + 1] = 1 + (i + 2) % n;
                output[3 * i + 2] = 1 + (i + 1) % n;
            }

            for (var i = 0; i < n; i++)
            {
                /*if (vertices[(1 + i + 1) % n].x  - vertices[1 + i].x <= 0)
                {
                    output[3 * n + 6 * i] = 1 + i;
                    output[3 * n + 6 * i + 1] = 1 + n + (i + 1) % n;
                    output[3 * n + 6 * i + 2] = 1 + (i + 1) % n;

                    output[3 * n + 6 * i + 3] = 1 + i;
                    output[3 * n + 6 * i + 4] = 1 + n + i;
                    output[3 * n + 6 * i + 5] = 1 + n + (i + 1) % n;
                }
                else
                {*/
                output[3 * n + 6 * i] = 1 + i;
                output[3 * n + 6 * i + 1] = 1 + (i + 1) % n;
                output[3 * n + 6 * i + 2] = 1 + n + (i + 1) % n;

                output[3 * n + 6 * i + 3] = 1 + i;
                output[3 * n + 6 * i + 4] = 1 + n + (i + 1) % n;
                output[3 * n + 6 * i + 5] = 1 + n + i;
                //}
            }

            return output;
        }
    }
}