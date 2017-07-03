using System.Collections.Generic;
using UnityEngine;

namespace Assets.Modules.Turrets.Vision
{
    public class DiamondDetector : MonoBehaviour
    {
        protected readonly List<GameObject> CollidingGameObjects = new List<GameObject>();

        protected MeshCollider ConeOfVisionMeshCollider;

        public float Height = 1;
        public float Length = 4;
        public float Width = 4;

        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
            ConeOfVisionMeshCollider = gameObject.GetComponent<MeshCollider>();
            if (ConeOfVisionMeshCollider == null)
            {
                ConeOfVisionMeshCollider = gameObject.AddComponent<MeshCollider>();
                ConeOfVisionMeshCollider.convex = true;
                ConeOfVisionMeshCollider.isTrigger = true;
            }
            ConeOfVisionMeshCollider.sharedMesh = CreateDiamondMesh();
        }

        protected virtual Mesh CreateDiamondMesh()
        {
            var mesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(0, -Height/2, Length/2),
                    new Vector3(Width, 0, Length/2),
                    new Vector3(0, Height, Length/2),
                    new Vector3(-Width, 0, Length/2),
                    new Vector3(0, 0, Length)
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            CollidingGameObjects.Add(other.gameObject);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            var localPosition = gameObject.transform.InverseTransformPoint(other.transform.position);
            if (CollidingGameObjects.Contains(other.gameObject) &&
                Mathf.Abs(localPosition.x/(Width/2))
                + Mathf.Abs(localPosition.y/(Height/2))
                + Mathf.Abs(localPosition.z/(Length/2)) > 0.95)
            {
                CollidingGameObjects.Remove(other.gameObject);
            }
        }

        protected virtual void OnValidate()
        {
            Awake();
        }
    }
}