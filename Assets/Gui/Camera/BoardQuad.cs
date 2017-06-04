using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class BoardQuad: MonoBehaviour
    {
        private MeshRenderer _renderer;
        private void Awake()
        {
            _renderer = gameObject.AddComponent<MeshRenderer>();
        }

        public void UpdateUniforms(int x, int y, int fragmetnsPerPixel)
        {
            _renderer.material.SetInt("_CameraPositionX", x);
            _renderer.material.SetInt("_CameraPositionY", y);
            _renderer.material.SetInt("_PixelSize", fragmetnsPerPixel);
        }
    }
}