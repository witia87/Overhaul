using Assets.Gui;
using UnityEngine;

namespace Assets.MainCamera
{
    public class CameraComponent : MonoBehaviour, ICameraStore
    {
        private GameObject _cameraHook;

        private IGuiStore _guiStore;

        [SerializeField] private float _pixelsPerUnit;
        public GameObject FocusObject;

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

        public Vector3 TransformVectorToCameraSpace(Vector3 vector)
        {
            return _cameraHook.transform.TransformVector(vector);
        }

        public PixelatedPositionsCalculator Pixelation { get; private set; }
        public Camera MainCamera { get; private set; }

        public Vector3 CameraEulerAngles
        {
            get { return gameObject.transform.localEulerAngles; }
        }

        public Vector3 FocusPoint
        {
            get { return FocusObject.transform.position; }
        }

        public RaycastsHelper Raycasts { get; private set; }

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            return Pixelation.GetClosestPixelatedPosition(position);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            return Pixelation.GetPixelatedOffset(from, to);
        }

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiComponent>();

            MainCamera = GetComponent<Camera>();
            MainCamera.cameraType = CameraType.Game;
            MainCamera.orthographic = true;

            Raycasts = new RaycastsHelper(MainCamera);

            _cameraHook = new GameObject("Camera Hook");
            _cameraHook.transform.localEulerAngles = gameObject.transform.localEulerAngles;
            _cameraHook.transform.position = _cameraHook.transform.TransformPoint(new Vector3(0, 0, -10));

            Pixelation = new PixelatedPositionsCalculator(this, _cameraHook);
        }

        private void Start()
        {
            MainCamera.orthographicSize = FieldOfViewHeight/2;
            MainCamera.targetTexture = _guiStore.BoardTexture;
            MainCamera.aspect = FieldOfViewWidth/FieldOfViewHeight;
            MainCamera.targetTexture = _guiStore.BoardTexture;
            Update();
        }

        private void Update()
        {
            var position = FocusObject.transform.position - gameObject.transform.TransformDirection(Vector3.forward*30);
            MainCamera.transform.position = Pixelation.GetClosestPixelatedPosition(position);
        }
    }
}