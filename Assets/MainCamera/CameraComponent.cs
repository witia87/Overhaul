using Assets.Gui;
using UnityEngine;

namespace Assets.MainCamera
{
    public class CameraComponent : MonoBehaviour, ICameraStore
    {
        private RenderTexture _boardTexture;

        public PixelatedPositionsCalculator Pixelation { get; private set; }

        [SerializeField] private float _pixelsPerUnit;

        private GameObject _cameraHook;
        public GameObject FocusObject;
        private Camera _mainCamera;

        private IGuiStore _guiStore;

        public float PixelsPerUnit
        {
            get { return _pixelsPerUnit; }
        }

        public float FieldOfViewWidth
        {
            get { return _guiStore.BoardPixelWidth/PixelsPerUnit; }
        }

        public float FieldOfViewHeight
        {
            get { return _guiStore.BoardPixelHeight/PixelsPerUnit; }
        }

        public Vector3 CameraEulerAngles
        {
            get { return gameObject.transform.localEulerAngles; }
        }

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            return Pixelation.GetClosestPixelatedPosition(position);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            return Pixelation.GetPixelatedOffset(from, to);
        }

        public Vector3 FocusPoint
        {
            get { return FocusObject.transform.position; }
        }

        public RaycastsHelper Raycasts { get; private set; }

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiComponent>() as IGuiStore;

            _mainCamera = GetComponent<Camera>();
            _mainCamera.cameraType = CameraType.Game;
            _mainCamera.orthographic = true;

            Raycasts = new RaycastsHelper(_mainCamera);

            _cameraHook = new GameObject("Camera Hook");
            _cameraHook.transform.eulerAngles = gameObject.transform.localEulerAngles;
            _cameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, -10));

            Pixelation = new PixelatedPositionsCalculator(this, _cameraHook);
        }

        private void Start()
        {
            _mainCamera.orthographicSize = FieldOfViewHeight/2;
            _mainCamera.targetTexture = _boardTexture;
            _mainCamera.aspect = FieldOfViewWidth/FieldOfViewHeight;
            _mainCamera.targetTexture = _guiStore.RegisterBoardTexture;
            Update();
        }

        private void Update()
        {
            var position = FocusObject.transform.position - gameObject.transform.TransformDirection(Vector3.forward*30);
            _mainCamera.transform.position = Pixelation.GetClosestPixelatedPosition(position);
        }
    }
}