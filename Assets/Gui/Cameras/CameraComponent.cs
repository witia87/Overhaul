using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Gui.Cameras
{
    public class CameraComponent : MonoBehaviour, ICameraStore
    {
        private GameObject _cameraHook;

        private IGuiStore _guiStore;

        //[SerializeField] private float _pixelsPerOneUnitInHeight;

        [SerializeField] private float _pixelsPerUnitInCameraSpace;

        [SerializeField] private LayerMask _emptyTargetingLayerMask;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private LayerMask _environmentLayerMask;

        public GameObject FocusObject;

        public float PixelsPerUnitInCameraSpace
        {
            get { return _pixelsPerUnitInCameraSpace; }
        }

        public Vector3 TransformVectorToCameraSpace(Vector3 vector)
        {
            return _cameraHook.transform.TransformVector(vector);
        }

        public PixelatedPositionsCalculator Pixelation { get; private set; }
        public Camera MainCamera { get; private set; }

        public Vector3 CameraEulerAngles
        {
            get { return transform.localEulerAngles; }
        }

        public Vector3 FocusPoint
        {
            get { return FocusObject.transform.position; }
        }

        public RaycastsHelper Raycasts { get; private set; }

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiComponent>();

            MainCamera = GetComponent<Camera>();
            MainCamera.cameraType = CameraType.Game;
            MainCamera.orthographic = true;

            Raycasts = new RaycastsHelper(MainCamera, _emptyTargetingLayerMask, _targetLayerMask, _environmentLayerMask);

            _cameraHook = new GameObject("Camera Hook");
            _cameraHook.transform.localEulerAngles = transform.localEulerAngles;
            _cameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, 0));

            //PixelsPerUnitInCameraSpace = _pixelsPerOneUnitInHeight / Mathf.Cos(CameraEulerAngles.x * Mathf.Deg2Rad);

            Pixelation = new PixelatedPositionsCalculator(this, _cameraHook);
        }

        private void Start()
        {
            MainCamera.orthographicSize = _guiStore.BoardPixelHeight / _pixelsPerUnitInCameraSpace / 2;
            MainCamera.targetTexture = _guiStore.BoardTexture;
            MainCamera.aspect = _guiStore.BoardPixelWidth /(float)_guiStore.BoardPixelHeight;
            Update();
            GetComponentInChildren<OutlineCameraComponent>().Initialize();
            //MainCamera.SetReplacementShader(Resources.Load("Materials/Shaders/Outline", typeof(Shader)) as Shader, "Outline");
        }

        public Vector3 WorldCameraFocusPoint;
        public Vector3 CameraPlaneOffset;

        private Vector3 _velocity;
        private void Update()
        {
            RaycastHit hit;
            Raycasts.ScreenPointToEmptyTargetingRay(Input.mousePosition, out hit);
            var targetPoint = FocusObject.transform.position +
                              (hit.point - FocusObject.transform.position).normalized * Mathf.Min(2, (hit.point - FocusObject.transform.position).magnitude / 2);
            WorldCameraFocusPoint = Vector3.SmoothDamp(WorldCameraFocusPoint, targetPoint, ref _velocity, 1);
            MainCamera.transform.position = Pixelation.GetClosestPixelatedPosition(WorldCameraFocusPoint);
            CameraPlaneOffset = _cameraHook.transform.InverseTransformPoint(WorldCameraFocusPoint) -
                                _cameraHook.transform.InverseTransformPoint(MainCamera.transform.position);
        }
    }
}