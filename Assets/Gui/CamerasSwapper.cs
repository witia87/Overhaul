using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class CamerasSwapper : MonoBehaviour
    {
        private RenderTexture _boardTexture;
        [SerializeField] private Camera _guiCamera;
        private bool _isRenderingToTexture = true;
        [SerializeField] private Camera _mainCamera;

        private float _swapCamerasCooldown;

        public void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                if (_isRenderingToTexture)
                {
                    _swapCamerasCooldown = 0.1f;
                    Camera.SetupCurrent(_mainCamera);
                    _boardTexture = _mainCamera.targetTexture;
                    _mainCamera.targetTexture = null;
                    _isRenderingToTexture = false;
                }
                else
                {
                    _swapCamerasCooldown = 0.1f;
                    _mainCamera.targetTexture = _boardTexture;
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