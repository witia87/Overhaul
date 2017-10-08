using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraComponent : MonoBehaviour
    {
        private GuiStore _guiStore;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiStore>();
            Camera = GetComponent<Camera>();
        }

        public void Start()
        {
            Camera.orthographicSize = _guiStore.BoardPixelHeight / _guiStore.PixelsPerUnitInCameraSpace / 2;
            Camera.aspect = _guiStore.BoardPixelWidth / (float)_guiStore.BoardPixelHeight;

            Camera.targetTexture.width = _guiStore.BoardPixelWidth;
            Camera.targetTexture.height = _guiStore.BoardPixelHeight;
        }
    }
}