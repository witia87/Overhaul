using Assets.MainCamera;
using UnityEngine;

namespace Assets.Gui
{
    public class CamerasSwapper : MonoBehaviour
    {
        private RenderTexture _boardTexture;
        private ICameraStore _cameraStore;
        private Camera _guiCamera;

        private IGuiStore _guiStore;
        private bool _isRenderingToTexture = true;
        private Camera _mainCamera;

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

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiComponent>();
            _cameraStore = FindObjectOfType<CameraComponent>();
        }

        private void Start()
        {
            _guiCamera = _guiStore.GuiCamera;
            _mainCamera = _cameraStore.MainCamera;
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