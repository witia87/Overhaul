using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class CamerasSwapper : MonoBehaviour
    {
        private RenderTexture _boardTexture;
        [SerializeField] private Camera _guiCamera;
        private bool _isRenderingToTexture = true;
        [SerializeField] private Camera _raycastCamera;

        private float _swapCamerasCooldown;

        public void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                _swapCamerasCooldown = 0.1f;
                if (_isRenderingToTexture)
                {
                    Camera.SetupCurrent(_raycastCamera);
                    _isRenderingToTexture = false;
                }
                else
                {
                    Camera.SetupCurrent(_guiCamera);
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