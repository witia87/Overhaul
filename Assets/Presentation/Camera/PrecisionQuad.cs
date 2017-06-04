using Assets.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Presentation.Camera
{
    public class PrecisionQuad
    {
        private readonly Vector3[] _normals = new Vector3[4] {Vector3.back, Vector3.back, Vector3.back, Vector3.back};
        private readonly int[] _triangles = new int[6] {0, 3, 1, 0, 2, 3};

        private readonly Vector2[] _uvs = new Vector2[4]
        {new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f)};

        private readonly Vector3[] _vertices = new Vector3[4] {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};

        private Material _material;
        private MeshRenderer _renderer;

        public void SetVertices(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
        {
            _vertices[0] = bottomLeft;
            _vertices[1] = bottomRight;
            _vertices[2] = topLeft;
            _vertices[3] = topRight;
        }

        public void Initialize(string name, Material material, GameObject parent, Vector3 bottomLeft,
            Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
        {
            SetVertices(bottomLeft, bottomRight, topLeft, topRight);
            _material = material;
            var currentObject = new GameObject(name);
            var mesh = new Mesh
            {
                vertices = _vertices,
                triangles = _triangles,
                uv = _uvs,
                normals = _normals
            };

            _renderer = currentObject.AddComponent<MeshRenderer>();
            currentObject.AddComponent<MeshFilter>().mesh = mesh;
            _renderer.material = _material;
            _renderer.shadowCastingMode = ShadowCastingMode.Off;
            _renderer.receiveShadows = false;

            currentObject.transform.parent = parent.transform;
            currentObject.transform.localPosition = Vector3.back;
            currentObject.transform.localEulerAngles = Vector3.zero;

            currentObject.layer = Layers.BuiltinLayer1;
        }

        public void UpdateUniforms(int x, int y, int fragmetnsPerPixel)
        {
            _renderer.material.SetInt("_CameraPositionX", x);
            _renderer.material.SetInt("_CameraPositionY", y);
            _renderer.material.SetInt("_PixelSize", fragmetnsPerPixel);
        }
    }
}