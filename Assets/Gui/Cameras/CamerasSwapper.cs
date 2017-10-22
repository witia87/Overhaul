using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CamerasSwapper : MonoBehaviour
    {
        [SerializeField] private Camera _debugCamera;
        private bool _isRenderingToTexture = true;

        private float _swapCamerasCooldown;
        [SerializeField] private Camera _viewCamera;

        public void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                _swapCamerasCooldown = 0.1f;
                if (_isRenderingToTexture)
                {
                    Camera.SetupCurrent(_debugCamera);
                    _viewCamera.enabled = false;
                    _isRenderingToTexture = false;
                }
                else
                {
                    Camera.SetupCurrent(_viewCamera);
                    _viewCamera.enabled = true;
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