using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class GuiInitializer: MonoBehaviour
    {
        CameraStore CameraStore;
        GameObject CameraHook;
        UnityEngine.Camera MainCamera;
        UnityEngine.Camera GuiCamera;
        public int PixelationSize = 2;
        public float PixelsPerUnit = 64;
        public Vector3 CameraEulerAngles = new Vector3(30,45,0);

        private GameObject _board;
        private RenderTexture _boardTexture;
        
        private GameObject _cameraHook;
        
        private bool _isRenderingToTexture = true;
        private float _swapCamerasCooldown;
        public Material BoardMaterial;

        private void Awake()
        {
            var pixelsInWidth = Screen.width / PixelationSize;
            var pixelsInHeight = Screen.height / PixelationSize;
            var boardWidth = pixelsInWidth / PixelsPerUnit;
            var boardHeight = pixelsInHeight / PixelsPerUnit;
            var aspect = pixelsInWidth / (float)pixelsInHeight;

            var singleFragmentSizeInUnits = 1 / (PixelsPerUnit * PixelationSize);
            var screenWidth = Screen.width * singleFragmentSizeInUnits;
            var screenHeight = Screen.height * singleFragmentSizeInUnits;

            _boardTexture = new RenderTexture(pixelsInWidth, pixelsInHeight, 256);
            _boardTexture.filterMode = FilterMode.Point;
            
            var boardMaterial = Resources.Load("Materials/BoardMaterial", typeof(Material)) as Material;
            boardMaterial.mainTexture = _boardTexture;
            _board = PrecisionQuadFactory.Create("Board Quad", boardMaterial, gameObject, Vector3.back,
                new Vector3(-boardWidth / 2, -boardHeight / 2, 0),
                new Vector3(+boardWidth / 2, -boardHeight / 2, 0),
                new Vector3(-boardWidth / 2, +boardHeight / 2, 0),
                new Vector3(+boardWidth / 2, +boardHeight / 2, 0));

            _camera = GetComponent<UnityEngine.Camera>();
            _camera.cameraType = CameraType.Game;
            _camera.orthographic = true;
            _camera.transform.position = GetCameraPosition(FocusObject.transform.position);
            _camera.orthographicSize = boardHeight / 2;
            _camera.targetTexture = _boardTexture;
            _camera.aspect = aspect;

            var guiBackgroundMaterial = Resources.Load("Materials/GuiBackgroundMaterial", typeof(Material)) as Material;
            boardMaterial.mainTexture = _boardTexture;
            _board = PrecisionQuadFactory.Create("Gui Backround Quad", guiBackgroundMaterial, gameObject, Vector3.back / 2,
                new Vector3(-screenWidth / 2, -screenHeight / 2, 0),
                new Vector3(+screenWidth / 2, -screenHeight / 2, 0),
                new Vector3(-screenWidth / 2, +screenHeight / 2, 0),
                new Vector3(+screenWidth / 2, +screenHeight / 2, 0));

            _guiCamera = new GameObject().AddComponent<UnityEngine.Camera>();
            _guiCamera.transform.parent = _camera.transform;
            _guiCamera.cameraType = CameraType.Preview;
            _guiCamera.orthographic = true;
            _guiCamera.transform.localPosition = new Vector3(0, 0, -10);
            _guiCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            _guiCamera.orthographicSize = screenHeight / 2;
            _guiCamera.aspect = screenWidth / screenHeight;
        }

        private void OnValidate()
        {
            SetupHook();
        }

        private void SetupHook()
        {
            CameraHook = new GameObject("Camera Hook");
            CameraHook.transform.eulerAngles = CameraEulerAngles;
            CameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, -10));
        }
    }
}