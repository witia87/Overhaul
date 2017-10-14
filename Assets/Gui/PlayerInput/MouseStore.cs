using Assets.Gui.Cameras;
using Assets.Gui.View;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class MouseStore : GuiStore, IMouseStore
    {
        private CameraStore _cameraStore;

        private Vector3 _mousePositionInBoardSpace;
        private ViewStore _viewStore;

        public Vector2 MousePositionInBoardSpace
        {
            get { return _mousePositionInBoardSpace; }
        }

        public bool IsMousePressed { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _viewStore = FindObjectOfType<ViewStore>();
            _cameraStore = FindObjectOfType<CameraStore>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            var x = Input.mousePosition.x / _viewStore.PixelizationSize;
            var y = Input.mousePosition.y / _viewStore.PixelizationSize;

            _mousePositionInBoardSpace = _cameraStore.CameraPositionInCameraPlaneSpace
                                         - new Vector2(_viewStore.ScreenWidthInPixels / 2 - x,
                                             _viewStore.ScreenHeightInPixels / 2 - y);

            IsMousePressed = Input.GetButton("Main Trigger");
        }
    }
}