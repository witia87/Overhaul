using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiStore : MonoBehaviour
    {
        [SerializeField] private Camera _guiCamera;
        public Vector3 CameraPlaneOffset;
        public CameraStore CameraStore;

        public Camera MainCamera;
        public int OutlineSize = 1;

        public int PixelizationSize = 4;

        public float PixelsPerUnitInCameraSpace = 8;


        public Vector3 WorldCameraFocusPoint;

        public int BoardPixelWidth
        {
            get { return Screen.width / PixelizationSize * 2; }
        }

        public int BoardPixelHeight
        {
            get { return Screen.height / PixelizationSize * 2; }
        }

        public float ViewOrtographicSize
        {
            get { return Screen.height / (float) PixelizationSize / PixelsPerUnitInCameraSpace / 2; }
        }

        public float ViewOrtographicAspect
        {
            get { return Screen.width / (float) Screen.height;
            }
        }
        
        public RaycastingCameraComponent Raycasts { get; private set; }

        private void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
            Raycasts = FindObjectOfType<RaycastingCameraComponent>();
        }

        private void Update()
        {
            // Commenting the code below will tur off the camera smoothing and restore the full low pixel display effect.
            var offset = new Vector3(
                CameraStore.CameraPlaneOffset.x * PixelsPerUnitInCameraSpace,
                CameraStore.CameraPlaneOffset.y * PixelsPerUnitInCameraSpace,
                0);
            _guiCamera.transform.localPosition = offset;
        }
    }
}