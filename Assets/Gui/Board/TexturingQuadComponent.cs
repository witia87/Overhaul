using UnityEngine;

namespace Assets.Gui.Board
{
    public class TexturingQuadComponent : QuadComponent
    {
        protected override void Start()
        {
            base.Start();
            var renderer = GetComponent<Renderer>();
            renderer.material.SetInt("_TexWidth", BoardStore.BoardTextureWidth); 
            renderer.material.SetInt("_TexHeight", BoardStore.BoardTextureHeight);
        }
    }
}