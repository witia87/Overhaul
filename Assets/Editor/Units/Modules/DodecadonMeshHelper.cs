using System;
using UnityEngine;

namespace Assets.Editor.Units.Modules
{
    public class DodecadonMeshHelper
    {
        public static Mesh Create(Vector3 size,
            float flatPartsRatio, float cornersOffsetRatio,
            float topSpikeRatio, float bottomSpikeRatio)
        {
            if (topSpikeRatio < 0 || bottomSpikeRatio < 0 || topSpikeRatio + bottomSpikeRatio >= 1
                || cornersOffsetRatio >= 0.5f)
            {
                throw new ApplicationException("Invalid MeshHelperParameters");
            }

            size.x /= 2;
            size.z /= 2;
            var flat = flatPartsRatio / 2;
            var verts = new[]
            {
                new Vector3(0, 0, 0), // bottom spike

                new Vector3(1, bottomSpikeRatio, flat),
                new Vector3(1 - cornersOffsetRatio, bottomSpikeRatio, 1 - cornersOffsetRatio),
                new Vector3(flat, bottomSpikeRatio, 1),
                new Vector3(-flat, bottomSpikeRatio, 1),
                new Vector3(-1 + cornersOffsetRatio, bottomSpikeRatio, 1 - cornersOffsetRatio),
                new Vector3(-1, bottomSpikeRatio, flat),
                new Vector3(-1, bottomSpikeRatio, -flat),
                new Vector3(-1 + cornersOffsetRatio, bottomSpikeRatio, -1 + cornersOffsetRatio),
                new Vector3(-flat, bottomSpikeRatio, -1),
                new Vector3(flat, bottomSpikeRatio, -1),
                new Vector3(1 - cornersOffsetRatio, bottomSpikeRatio, -1 + cornersOffsetRatio),
                new Vector3(1, bottomSpikeRatio, -flat),
                new Vector3(1, 1 - topSpikeRatio, flat),
                new Vector3(1 - cornersOffsetRatio, 1 - topSpikeRatio, 1 - cornersOffsetRatio),
                new Vector3(flat, 1 - topSpikeRatio, 1),
                new Vector3(-flat, 1 - topSpikeRatio, 1),
                new Vector3(-1 + cornersOffsetRatio, 1 - topSpikeRatio, 1 - cornersOffsetRatio),
                new Vector3(-1, 1 - topSpikeRatio, flat),
                new Vector3(-1, 1 - topSpikeRatio, -flat),
                new Vector3(-1 + cornersOffsetRatio, 1 - topSpikeRatio, -1 + cornersOffsetRatio),
                new Vector3(-flat, 1 - topSpikeRatio, -1),
                new Vector3(flat, 1 - topSpikeRatio, -1),
                new Vector3(1 - cornersOffsetRatio, 1 - topSpikeRatio, -1 + cornersOffsetRatio),
                new Vector3(1, 1 - topSpikeRatio, -flat),
                new Vector3(0, 1, 0) // top spike
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
                    0, 8, 9,
                    0, 9, 10,
                    0, 10, 11,
                    0, 11, 12,
                    0, 12, 1,
                    1, 24, 13,
                    1, 13, 2,
                    2, 13, 14,
                    2, 14, 3,
                    3, 14, 15,
                    3, 15, 4,
                    4, 15, 16,
                    4, 16, 5,
                    5, 16, 17,
                    5, 17, 6,
                    6, 17, 18,
                    6, 18, 7,
                    7, 18, 19,
                    7, 19, 8,
                    8, 19, 20,
                    8, 20, 9,
                    9, 20, 21,
                    9, 21, 10,
                    10, 21, 22,
                    10, 22, 11,
                    11, 22, 23,
                    11, 23, 12,
                    12, 23, 24,
                    12, 24, 1,
                    25, 13, 14,
                    25, 14, 15,
                    25, 15, 16,
                    25, 16, 17,
                    25, 17, 18,
                    25, 18, 19,
                    25, 19, 20,
                    25, 20, 21,
                    25, 21, 22,
                    25, 22, 23,
                    25, 23, 24,
                    25, 24, 13
                }
            };
            mesh.RecalculateBounds();
            mesh.MarkDynamic();
            return mesh;
        }
    }
}