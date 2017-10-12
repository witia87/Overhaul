using UnityEngine;

namespace Assets.Gui.Board
{
    public class QuadComponent : MonoBehaviour
    {
        private static readonly Vector3[] Normals = new Vector3[4]
            {Vector3.back, Vector3.back, Vector3.back, Vector3.back};

        private static readonly int[] Triangles = new int[6] {0, 3, 1, 0, 2, 3};

        private static readonly Vector2[] Uvs = new Vector2[4]
            {new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f)};


        protected BoardStore GuiStore;
        protected virtual void Awake()
        {
            GuiStore = FindObjectOfType<BoardStore>();
        }

        protected virtual void Start()
        {
            var vertices = new Vector3[4]
            {
                new Vector3(-GuiStore.BoardTextureWidth / (float) 2, -GuiStore.BoardTextureHeight / (float) 2, 0),
                new Vector3(+GuiStore.BoardTextureWidth / (float) 2, -GuiStore.BoardTextureHeight / (float) 2, 0),
                new Vector3(-GuiStore.BoardTextureWidth / (float) 2, +GuiStore.BoardTextureHeight / (float) 2, 0),
                new Vector3(+GuiStore.BoardTextureWidth / (float) 2, +GuiStore.BoardTextureHeight / (float) 2, 0)
            };
            var mesh = new Mesh
            {
                vertices = vertices,
                triangles = Triangles,
                uv = Uvs,
                normals = Normals
            };

            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}