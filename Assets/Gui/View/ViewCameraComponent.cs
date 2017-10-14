using UnityEngine;

namespace Assets.Gui.View
{
    public class ViewCameraComponent : GuiComponent
    {
        public Camera Camera { get; private set; }

        private void Start()
        {
            Camera = GetComponent<Camera>();
            UpdateCameraParameters();
            Camera.SetupCurrent(Camera);
        }

        private void UpdateCameraParameters()
        {
            Camera.orthographicSize = ViewStore.ScreenHeightInPixels / 2;
            Camera.aspect = Screen.width / (float) Screen.height;
        }
    }
}