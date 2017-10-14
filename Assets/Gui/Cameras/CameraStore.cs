using Assets.Gui.Payloads;
using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraStore : GuiStore, ICameraStore
    {
        [SerializeField] private readonly float _rescale = 8;

        public float Rescale
        {
            get { return _rescale; }
        }

        public PixelatedPositionsCalculator Pixelation { get; private set; }

        public Vector3 CameraEulerAngles
        {
            get { return transform.localEulerAngles; }
        }

        public Vector2 CameraPositionInCameraPlaneSpace { get; private set; }
        public Vector2 PixelatedCameraPositionInBoardSpace { get; private set; }

        public Vector2 TransformWorldPositionToCameraPlane(Vector3 worldPosition)
        {
            return transform.InverseTransformPoint(worldPosition);
        }

        public Ray TransformCameraPlanePositionToWorldRay(Vector2 cameraPlanePosition)
        {
            var pointToCast = transform.TransformPoint(cameraPlanePosition);
            var currentHeight = pointToCast.y;
            var unitHeight = -transform.forward.y;
            var castedPoint = pointToCast + transform.forward * (currentHeight - 10 /*fixed height*/) / unitHeight;
            return new Ray(castedPoint, transform.forward);
        }

        protected override void Awake()
        {
            base.Awake();
            Pixelation = new PixelatedPositionsCalculator(this);
            transform.localScale = new Vector3(1 / Rescale, 1 / Rescale, 1);
        }

        protected void Start()
        {
            Dispatcher.Register(GuiCommandIds.ChangeFocusPointInBoardSpace,
                payload => OnMousePositionChanged((payload as ChangeFocusPointInBoardSpacePayload).Position));
        }

        private void OnMousePositionChanged(Vector2 focusPointOnCameraPlanePosition)
        {
            CameraPositionInCameraPlaneSpace = focusPointOnCameraPlanePosition;
            PixelatedCameraPositionInBoardSpace = new Vector2(
                Mathf.Round(focusPointOnCameraPlanePosition.x),
                Mathf.Round(focusPointOnCameraPlanePosition.y));
        }
    }
}