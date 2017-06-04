using Assets.Flux;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class CameraComponent : MonoBehaviour
    {
        private static CameraComponent _instance;

        private GameObject _board;
        private RenderTexture _boardTexture;

        private UnityEngine.Camera _camera;

        private GameObject _cameraHook;

        private ICameraStore _cameraStore;
        private UnityEngine.Camera _guiCamera;

        private bool _isRenderingToTexture = true;
        private float _swapCamerasCooldown;
        public Material BoardMaterial;

        public GameObject FocusObject;
        public int PixelationSize = 2;
        public float PixelationsPixelsPerUnit = 64;

        public static float PixelsPerUnit
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraComponent>();
                }
                return _instance.PixelationsPixelsPerUnit;
            }
        }

        public static Vector3 CameraEulerAngles
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraComponent>();
                }
                return _instance.gameObject.transform.localEulerAngles;
            }
        }

        public static UnityEngine.Camera GuiCamera
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraComponent>();
                }
                return _instance._guiCamera;
            }
        }



        private void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                if (_isRenderingToTexture)
                {
                    _swapCamerasCooldown = 0.1f;
                    _guiCamera.enabled = !_guiCamera.enabled;
                    _camera.targetTexture = null;
                    _isRenderingToTexture = false;
                }
                else
                {
                    _swapCamerasCooldown = 0.1f;
                    _guiCamera.enabled = !_guiCamera.enabled;
                    _camera.targetTexture = _boardTexture;
                    _isRenderingToTexture = true;
                }
            }
        }

        private void Update()
        {
            var focusObjectPosition = FocusObject.transform.position;
            gameObject.transform.position = GetCameraPosition(FocusObject.transform.position);
            GameMechanics.Dispatcher.Dispatch(Commands.SetCameraFocusPoint,
                new SetCameraFocusPointPayload {FocusPoint = focusObjectPosition});

            _swapCamerasCooldown = Mathf.Max(0, _swapCamerasCooldown - Time.deltaTime);
            if (Input.GetKey(KeyCode.F9))
            {
                SwapCameras();
            }
        }
    }
}