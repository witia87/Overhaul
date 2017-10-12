using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class GuiCameraComponent : MonoBehaviour
    {
        private GuiStore _guiStore;

        public Camera Camera { get; private set; }

        private void Start()
        {
            _guiStore = FindObjectOfType<GuiStore>();
            Camera = GetComponent<Camera>();
            UpdateCameraParameters();
            Camera.SetupCurrent(Camera);
        }

        private void UpdateCameraParameters()
        {
            Camera.orthographicSize = _guiStore.ScreenHeightInPixels/2;
            Camera.aspect = Screen.width / (float)Screen.height;

        }
    }
}