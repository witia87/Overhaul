using UnityEngine;

namespace Assets.Editor.Modules
{
    public class ExtendedOctagonMeshHelper
    {
        public static Mesh Create(Vector3 size, float bottomSpikeRatio, float topSpikeRatio)
        {
            var sqrt = 1 / Mathf.Sqrt(2);
            size.x /= 2;
            size.z /= 2;
            var verts = new[]
            {
                new Vector3(0, 0, 0), //spike

                new Vector3(1, bottomSpikeRatio, 0),
                new Vector3(sqrt, bottomSpikeRatio, sqrt),
                new Vector3(0, bottomSpikeRatio, 1),
                new Vector3(-sqrt, bottomSpikeRatio, sqrt),
                new Vector3(-1, bottomSpikeRatio, 0),
                new Vector3(-sqrt, bottomSpikeRatio, -sqrt),
                new Vector3(0, bottomSpikeRatio, -1),
                new Vector3(sqrt, bottomSpikeRatio, -sqrt),
                new Vector3(1, 1 - topSpikeRatio, 0),
                new Vector3(sqrt, 1 - topSpikeRatio, sqrt),
                new Vector3(0, 1 - topSpikeRatio, 1),
                new Vector3(-sqrt, 1 - topSpikeRatio, sqrt),
                new Vector3(-1, 1 - topSpikeRatio, 0),
                new Vector3(-sqrt, 1 - topSpikeRatio, -sqrt),
                new Vector3(0, 1 - topSpikeRatio, -1),
                new Vector3(sqrt, 1 - topSpikeRatio, -sqrt),
                new Vector3(0, 1, 0)
            };
            for (var i = 0; i < verts.Length; i++)
            {
                verts[i].Scale(size);
            }
            var mesh = new Mesh
            {
                vertices = verts,
                triangles = new[]
                {
                    0, 1, 2,
                    0, 2, 3,
                    0, 3, 4,
                    0, 4, 5,
                    0, 5, 6,
                    0, 6, 7,
                    0, 7, 8,
                    0, 8, 1,
                    1, 16, 9,
                    1, 9, 2, //
                    2, 9, 10,
                    2, 10, 3, //
                    3, 10, 11,
                    3, 11, 4, //
                    4, 11, 12,
                    4, 12, 5, //
                    5, 12, 13,
                    5, 13, 6, //
                    6, 13, 14,
                    6, 14, 7, //
                    7, 14, 15,
                    7, 15, 8, //
                    8, 15, 16,
                    8, 16, 1, //


                    17, 9, 10,
                    17, 10, 11,
                    17, 11, 12,
                    17, 12, 13,
                    17, 13, 14,
                    17, 14, 15,
                    17, 15, 16,
                    17, 16, 9
                }
            };
            mesh.RecalculateBounds();
            mesh.MarkDynamic();
            return mesh;
        }
    }
}