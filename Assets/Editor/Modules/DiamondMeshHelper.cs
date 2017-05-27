using UnityEngine;

namespace Assets.Editor.Modules
{
    public class DiamondMeshHelper
    {
        private Mesh Create(float horizontalAngleTolerance, float verticalAngleTolerance, float visionLenght)
        {
            var mesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(0, -Mathf.Tan(verticalAngleTolerance)*visionLenght/2, visionLenght/2),
                    new Vector3(Mathf.Tan(horizontalAngleTolerance)*visionLenght/2, 0, visionLenght/2),
                    new Vector3(0, Mathf.Tan(verticalAngleTolerance)*visionLenght/2, visionLenght/2),
                    new Vector3(-Mathf.Tan(horizontalAngleTolerance)*visionLenght/2, 0, visionLenght/2),
                    new Vector3(0, 0, visionLenght)
                },
                triangles = new[]
                {
                    0, 1, 2,
                    0, 2, 3,
                    0, 3, 4,
                    0, 4, 1,
                    1, 5, 2,
                    2, 5, 3,
                    3, 5, 4,
                    4, 5, 1
                }
            };
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}