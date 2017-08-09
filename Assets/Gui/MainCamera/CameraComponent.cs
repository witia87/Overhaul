using UnityEngine;

namespace Assets.Gui.MainCamera
{
    public class CameraComponent : MonoBehaviour, ICameraStore
    {
        private GameObject _cameraHook;
        private IGuiStore _guiStore;

        [SerializeField] private float _pixelsPerOneUnitInHeight;

        public GameObject FocusObject;

        public float PixelsPerUnit { get; private set; }

        public float FieldOfViewWidth
        {
            get { return _guiStore.BoardPixelWidth/PixelsPerUnit; }
        }

        public float FieldOfViewHeight
        {
            get { return _guiStore.BoardPixelHeight/PixelsPerUnit; }
        }

        /// <summary>
        ///     This value informs how many pixels will be projected onto the screen in height, when the height of an object is 1
        ///     unit.
        /// </summary>
        public float PixelsPerOneUnitInHeight { get; private set; }

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

        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private LayerMask _targetLayerMask;
        public RaycastsHelper Raycasts { get; private set; }

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiComponent>();

            MainCamera = GetComponent<Camera>();
            MainCamera.cameraType = CameraType.Game;
            MainCamera.orthographic = true;

            Raycasts = new RaycastsHelper(MainCamera, _floorLayerMask, _targetLayerMask);

            _cameraHook = new GameObject("Camera Hook");
            _cameraHook.transform.localEulerAngles = transform.localEulerAngles;
            _cameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, 0));

            PixelsPerUnit = _pixelsPerOneUnitInHeight/Mathf.Cos(CameraEulerAngles.x*Mathf.Deg2Rad);

            Pixelation = new PixelatedPositionsCalculator(this, _cameraHook);
        }

        private void Start()
        {
            MainCamera.orthographicSize = FieldOfViewHeight/2;
            MainCamera.targetTexture = _guiStore.BoardTexture;
            MainCamera.aspect = FieldOfViewWidth/FieldOfViewHeight;
            MainCamera.targetTexture = _guiStore.BoardTexture;
            Update();
            //MainCamera.SetReplacementShader(Resources.Load("Materials/Shaders/Outline", typeof(Shader)) as Shader, "Outline");
        }

        private void Update()
        {
            var position = FocusObject.transform.position - transform.TransformDirection(Vector3.forward*30);
            MainCamera.transform.position = Pixelation.GetClosestPixelatedPosition(position);
        }
    }
}