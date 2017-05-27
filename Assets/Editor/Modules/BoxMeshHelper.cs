using UnityEngine;

namespace Assets.Editor.Modules
{
    public class BoxMeshHelper
    {
        public static Mesh Create(float width, float height, float length, float spikeRatio)
        {
            var spikeHeight = spikeRatio*height;
            var mesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(0, 0, 0), //spike

                    new Vector3(-width/2, spikeHeight, -length/2),
                    new Vector3(-width/2, spikeHeight, length/2),
                    new Vector3(width/2, spikeHeight, length/2),
                    new Vector3(width/2, spikeHeight, -length/2),
                    new Vector3(-width/2, height, -length/2),
                    new Vector3(-width/2, height, length/2),
                    new Vector3(width/2, height, length/2),
                    new Vector3(width/2, height, -length/2)
                },
                triangles = new[]
                {
                    0, 1, 2, // back spike
                    0, 2, 3, // right spike
                    0, 3, 4, // front spike
                    0, 4, 1, // left spike

                    1, 5, 2, // back face
                    2, 5, 6,
                    2, 6, 3, // right face
                    3, 6, 7,
                    3, 7, 4, // front face
                    4, 7, 8,
                    4, 8, 1, // left face
                    1, 8, 5,
                    5, 7, 6,
                    5, 8, 7
                }
            };
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}