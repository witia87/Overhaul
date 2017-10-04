using System.Collections.Generic;
using Assets.Gui.Cameras;
using Assets.Sight;
using Assets.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Gui
{
    public class SightComponent: MonoBehaviour
    {
        private MeshFilter _meshFilter;

        private SightStore _sightStore;
        private CameraComponent _cameraComponent;
        private GuiComponent _guiComponent;

        public void Awake()
        {
            _sightStore = FindObjectOfType<SightStore>();
            _cameraComponent = FindObjectOfType<CameraComponent>();
            _guiComponent = FindObjectOfType<GuiComponent>();

            _meshFilter = GetComponent<MeshFilter>();

            var quadGameObject = new GameObject(name);
            var mesh = new Mesh();
            

            _meshFilter.mesh = mesh;

            quadGameObject.layer = Layers.BuiltinLayer1;
        }

        private void Update()
        {
            var polygon = _sightStore.GetSightPolygon();
            var vertices = GetVertices(_sightStore.Center, polygon);
            _meshFilter.mesh.Clear();
            _meshFilter.mesh.vertices = vertices;
            _meshFilter.mesh.uv = GetUvs(vertices);
            _meshFilter.mesh.triangles = GenerateTriangulation(polygon.Count);
            _meshFilter.mesh.normals = GenerateNormals(polygon.Count);
            
        }

        private Vector3[] GetVertices(Vector2 center, List<Vector2> triangulation)
        {
            var output = new Vector3[triangulation.Count + 1];
            output[0] = _cameraComponent.Pixelation.GetScreenPosition(new Vector3(center.x, 0, center.y));
            for (int i = 0; i < triangulation.Count; i++)
            {
                output[i + 1] = _cameraComponent.Pixelation.GetScreenPosition(new Vector3(triangulation[i].x, 0, triangulation[i].y));
            }
            return output;
        }

        private Vector2[] GetUvs(Vector3[] vertices)
        {
            var output = new Vector2[vertices.Length];
            var divisor = new Vector2(_guiComponent.BoardPixelWidth, _guiComponent.BoardPixelWidth);
            for (int i = 0; i < vertices.Length; i++)
            {
                output[i] = new Vector2(vertices[i].x / divisor.x, vertices[i].y / divisor.y);
            }
            return output;
        }

        private int[] GenerateTriangulation(int n)
        {
            var output = new int[3 * n];
            for (int i = 0; i < n; i++)
            {
                output[3 * i] = 0;
                output[3 * i + 1] = 1 + ((i + 2) % n);
                output[3 * i + 2] = 1 + ((i + 1) % n);
            }
            return output;
        }

        private Vector3[] GenerateNormals(int n)
        {
            var output = new Vector3[n+1];
            for (int i = 0; i < n + 1; i++)
            {
                output[i] = Vector3.back;
            }
            return output;
        }
    }
}