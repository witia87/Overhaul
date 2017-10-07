using System.Collections.Generic;
using Assets.Gui.Cameras;
using Assets.Sight;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Gui
{
    public class SightPolygonComponent : MonoBehaviour
    {
        private CameraComponent _cameraComponent;
        private GuiStore _guiStore;
        private MeshFilter _meshFilter;

        private SightStore _sightStore;

        public void Awake()
        {
            _sightStore = FindObjectOfType<SightStore>();
            _cameraComponent = FindObjectOfType<CameraComponent>();
            _guiStore = FindObjectOfType<GuiStore>();

            _meshFilter = GetComponent<MeshFilter>();
            
            var mesh = new Mesh();

            _meshFilter.mesh = mesh;

        }
        
        private void Update()
        {
            for (var i = 0; i < 20; i++) // TODO: overload on purpouse; remove later
            {
                var polygon = _sightStore.GetSightPolygon();
                var vertices = GetExtendedVertices(_sightStore.Center, polygon);
                _meshFilter.mesh.Clear();
                _meshFilter.mesh.vertices = vertices;
                _meshFilter.mesh.uv = GetUvs(vertices);
                _meshFilter.mesh.triangles = GenerateExtendedTriangulation(polygon.Count);
                _meshFilter.mesh.normals = GenerateNormals(vertices);
            }
        }

        private Vector3[] GetVertices(Vector2 center, List<Vector2> polygon)
        {
            var output = new Vector3[polygon.Count + 1];
            output[0] = _cameraComponent.Pixelation.GetScreenPosition(new Vector3(center.x, 0, center.y));
            for (var i = 0; i < polygon.Count; i++)
            {
                output[i + 1] =
                    _cameraComponent.Pixelation.GetScreenPosition(new Vector3(polygon[i].x, 0, polygon[i].y));
            }
            return output;
        }

        private Vector3[] GetExtendedVertices(Vector2 center, List<Vector2> polygon)
        {
            var output = new Vector3[polygon.Count + 1 + polygon.Count];
            output[0] = _cameraComponent.Pixelation.GetScreenPosition(new Vector3(center.x, 0, center.y));

            var offset = _cameraComponent.Pixelation.GetScreenOffset(Vector3.zero, Vector3.up * 2);
            for (var i = 0; i < polygon.Count; i++)
            {
                output[i + 1] =
                    _cameraComponent.Pixelation.GetScreenPosition(new Vector3(polygon[i].x, 0, polygon[i].y));
                output[1 + polygon.Count + i] = _cameraComponent.Pixelation.GetScreenPosition(new Vector3(polygon[i].x, 2, polygon[i].y));
            }
            return output;
        }

        private Vector2[] GetUvs(Vector3[] vertices)
        {
            var output = new Vector2[vertices.Length];
            var divisor = new Vector2(_guiStore.BoardPixelWidth, _guiStore.BoardPixelHeight);
            for (var i = 0; i < vertices.Length; i++)
            {
                output[i] = new Vector2(0.5f + vertices[i].x / divisor.x, 0.5f + vertices[i].y / divisor.y);  // BUG HERE
            }
            return output;
        }

        private int[] GenerateTriangulation(int n)
        {
            var output = new int[3 * n];
            for (var i = 0; i < n; i++)
            {
                output[3 * i] = 0;
                output[3 * i + 1] = 1 + (i + 2) % n;
                output[3 * i + 2] = 1 + (i + 1) % n;
            }
            return output;
        }

        private int[] GenerateExtendedTriangulation(int n)
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
                output[3 * n + 6 * i] = 1 + i;
                output[3 * n + 6 * i + 1] = 1 + n + (i + 1) % n;
                output[3 * n + 6 * i + 2] = 1 + (i + 1) % n;

                output[3 * n + 6 * i + 3] = 1 + i;
                output[3 * n + 6 * i + 4] = 1 + n + i;
                output[3 * n + 6 * i + 5] = 1 + n + (i + 1) % n;
            }

            return output;
        }

        private Vector3[] GenerateNormals(Vector3[] vertices)
        {
            var output = new Vector3[vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                output[i] = Vector3.back;
            }
            return output;
        }
    }
}