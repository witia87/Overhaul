using UnityEngine;

namespace Assets.Gui.Board
{
    public class OutlineQuadComponent : QuadComponent
    {
        protected override void Start()
        {
            base.Start();
            var renderer = GetComponent<Renderer>();
            renderer.material.mainTexture.width = GuiStore.BoardPixelWidth;
            renderer.material.mainTexture.height = GuiStore.BoardPixelHeight;
            // TODO: This depends on the texture Initializations performed in Cameras
            // TODO: Move texture sizes initialization to the Gui (QuadComponents?)
            renderer.material.SetInt("_TexWidth", renderer.material.mainTexture.width); 
            renderer.material.SetInt("_TexHeight", renderer.material.mainTexture.height);
        }
    }
}