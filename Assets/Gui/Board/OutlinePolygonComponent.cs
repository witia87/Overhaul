using UnityEngine;

namespace Assets.Gui.Board
{
    public class OutlinePolygonComponent: MonoBehaviour
    {
        private MeshFilter _meshFilter;

        public SightPolygonComponent SightPolygonComponent;

        private GuiStore _guiStore;
        protected void Start()
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.mainTexture.width = _guiStore.BoardPixelWidth;
            renderer.material.mainTexture.height = _guiStore.BoardPixelHeight;
            // TODO: This depends on the texture Initializations performed in Cameras
            // TODO: Move texture sizes initialization to the Gui (QuadComponents?)
            renderer.material.SetInt("_TexWidth", renderer.material.mainTexture.width);
            renderer.material.SetInt("_TexHeight", renderer.material.mainTexture.height);

            _meshFilter.sharedMesh = SightPolygonComponent.GetComponent<MeshFilter>().mesh;
        }

        public void Awake()
        {
            _guiStore = FindObjectOfType<GuiStore>();
            _meshFilter = GetComponent<MeshFilter>();
        }
    }
}