using Assets.Flux;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class CameraComponent : MonoBehaviour
    {
        private static CameraComponent _instance;

        private readonly PrecisionQuad _board = new PrecisionQuad();
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


        private void Awake()
        {
            _cameraStore = GameMechanics.Stores.CameraStore;
            GameMechanics.Dispatcher.Dispatch(Commands.InitializeCamera,
                new InitializeCameraPayload {CameraEulerAngles = gameObject.transform.eulerAngles});

            _cameraHook = new GameObject("Camera Hook");
            _cameraHook.transform.eulerAngles = CameraEulerAngles;
            _cameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, -10));

            var pixelsInWidth = Screen.width/PixelationSize;
            var pixelsInHeight = Screen.height/PixelationSize;
            var boardWidth = pixelsInWidth/PixelsPerUnit;
            var boardHeight = pixelsInHeight/PixelsPerUnit;
            var ascpect = pixelsInWidth/(float) pixelsInHeight;

            _boardTexture = new RenderTexture(pixelsInWidth, pixelsInHeight, 256);
            _boardTexture.filterMode = FilterMode.Point;

            var boardMaterial = Resources.Load("Materials/BoardMaterial", typeof (Material)) as Material;
            boardMaterial.mainTexture = _boardTexture;
            _board.Initialize("BoardQuad", boardMaterial, gameObject,
                new Vector3(-boardWidth/2, -boardHeight/2, 0),
                new Vector3(+boardWidth/2, -boardHeight/2, 0),
                new Vector3(-boardWidth/2, +boardHeight/2, 0),
                new Vector3(+boardWidth/2, +boardHeight/2, 0));

            _camera = GetComponent<UnityEngine.Camera>();
            _camera.cameraType = CameraType.Game;
            _camera.orthographic = true;
            _camera.transform.position = GetCameraPosition(FocusObject.transform.position);
            _camera.orthographicSize = boardHeight/2;
            _camera.targetTexture = _boardTexture;
            _camera.aspect = ascpect;

            _guiCamera = new GameObject().AddComponent<UnityEngine.Camera>();
            _guiCamera.transform.parent = _camera.transform;
            _guiCamera.cameraType = CameraType.Preview;
            _guiCamera.orthographic = true;
            _guiCamera.transform.localPosition = new Vector3(0, 0, -10);
            _guiCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            _guiCamera.orthographicSize = boardHeight/2;
            _guiCamera.aspect = ascpect;
        }

        private void SwapCameras()
        {
            if (_swapCamerasCooldown <= 0)
            {
                _swapCamerasCooldown = 0.1f;
                _guiCamera.enabled = !_guiCamera.enabled;
                _camera.targetTexture = null;
            }
        }

        private Vector3 GetCameraPosition(Vector3 focusPoint)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(focusPoint);
            focusPointInCameraSpace.z = 0; // ortogonalProjectionToTheCameraPlane
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x*PixelationsPixelsPerUnit)/
                                        PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y*PixelationsPixelsPerUnit)/
                                        PixelationsPixelsPerUnit;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public static Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraComponent>();
            }
            var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(position);
            //focusPointInCameraSpace.z = 0; // ortogonalProjectionToTheCameraPlane
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x*_instance.PixelationsPixelsPerUnit)/
                                        _instance.PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y*_instance.PixelationsPixelsPerUnit)/
                                        _instance.PixelationsPixelsPerUnit;
            return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public static Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraComponent>();
            }
            var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(to - from);
            //focusPointInCameraSpace.z = 0; // ortogonalProjectionToTheCameraPlane
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
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
            //_board.UpdateUniforms(CameraControl.x, CameraControl.y, CameraControl.i);
        }
    }
}