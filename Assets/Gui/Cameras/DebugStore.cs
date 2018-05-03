using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class DebugStore : MonoBehaviour
    {
        public static bool IsDebugMode;
        [SerializeField] private Camera _debugCamera;

        private float _swapCamerasCooldown;
        [SerializeField] private Camera _viewCamera;

        private void Awake()
        {
            Camera.SetupCurrent(_viewCamera);
            _viewCamera.enabled = true;
        }

        public void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                _swapCamerasCooldown = 0.1f;
                if (!IsDebugMode)
                {
                    Camera.SetupCurrent(_debugCamera);
                    _viewCamera.enabled = false;
                    IsDebugMode = true;
                }
                else
                {
                    Camera.SetupCurrent(_viewCamera);
                    _viewCamera.enabled = true;
                    IsDebugMode = false;
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