using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class DebugCameraComponent : GuiComponent
    {
        public Camera Camera { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Camera = GetComponent<Camera>();
        }

        public void Start()
        {
            Camera.orthographicSize = ViewStore.ScreenHeightInPixels / CameraStore.Rescale / 2;
            Camera.aspect = ViewStore.ScreenWidthInPixels / ViewStore.ScreenHeightInPixels;
        }

        public void Update()
        {
            transform.localPosition = CameraStore.CameraPositionInCameraPlaneSpace;
        }
    }
}