using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class OutlineCameraComponent : MonoBehaviour
    {
        private CameraComponent _cameraStore;
        private GuiStore _guiStore;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _cameraStore = FindObjectOfType<CameraComponent>();
            _guiStore = FindObjectOfType<GuiStore>();

            Camera = GetComponent<Camera>();
            Camera.cameraType = CameraType.Game;
            Camera.orthographic = true;
        }

        public void Start()
        {
            Camera.orthographicSize = _guiStore.BoardPixelHeight / _guiStore.PixelsPerUnitInCameraSpace / 2;
            Camera.aspect = _guiStore.BoardPixelWidth / (float)_guiStore.BoardPixelHeight;
        }
    }
}