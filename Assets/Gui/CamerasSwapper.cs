using UnityEngine;

namespace Assets.Utilities
{
    public class CamerasSwapper: MonoBehaviour
    {
        public Camera GuiCamera;
        public Camera MainCamera;

        private float _swapCamerasCooldown;
        private bool _isRenderingToTexture = true;

        private RenderTexture _boardTexture;
        public void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                if (_isRenderingToTexture)
                {
                    _swapCamerasCooldown = 0.1f;
                    GuiCamera.enabled = !GuiCamera.enabled;
                    _boardTexture = MainCamera.targetTexture;
                    MainCamera.targetTexture = null;
                    _isRenderingToTexture = false;
                }
                else
                {
                    _swapCamerasCooldown = 0.1f;
                    GuiCamera.enabled = !GuiCamera.enabled;
                    MainCamera.targetTexture = _boardTexture;
                    _isRenderingToTexture = true;
                }
            }
        }

        private void Update()
        {
            _swapCamerasCooldown = Mathf.Max(0, _swapCamerasCooldown - Time.deltaTime);
            if (Input.GetKey(KeyCode.F9))
            {
                SwapCameras();
            }
        }
    }
}
