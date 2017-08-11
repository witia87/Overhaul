using Assets.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Gui
{
    public static class PrecisionQuadFactory
    {
        private static readonly Vector3[] _normals = new Vector3[4]
            {Vector3.back, Vector3.back, Vector3.back, Vector3.back};

        private static readonly int[] _triangles = new int[6] {0, 3, 1, 0, 2, 3};

        private static readonly Vector2[] _uvs = new Vector2[4]
            {new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f)};

        public static GameObject Create(string name, Material material,
            Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
        {
            var _vertices = new Vector3[4] {bottomLeft, bottomRight, topLeft, topRight};
            var quadGameObject = new GameObject(name);
            var mesh = new Mesh
            {
                vertices = _vertices,
                triangles = _triangles,
                uv = _uvs,
                normals = _normals
            };

            var renderer = quadGameObject.AddComponent<MeshRenderer>();
            quadGameObject.AddComponent<MeshFilter>().mesh = mesh;
            renderer.material = material;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.receiveShadows = false;

            quadGameObject.transform.localPosition = Vector3.zero;
            quadGameObject.transform.localEulerAngles = Vector3.zero;

            quadGameObject.layer = Layers.BuiltinLayer1;
            return quadGameObject;
        }
    }
}