using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class EnvironmentCameraComponent : MonoBehaviour
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
            Camera.targetTexture.width = _guiStore.BoardPixelWidth;
            Camera.targetTexture.height = _guiStore.BoardPixelHeight;

            Camera.transform.localEulerAngles = Vector3.zero;
            Camera.transform.localPosition = Vector3.zero;
            Camera.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}