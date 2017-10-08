using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraStore : MonoBehaviour
    {
        private GuiStore _guiStore;
        public Vector3 TransformVectorToCameraSpace(Vector3 vector)
        {
            return transform.TransformVector(vector);
        }

        public PixelatedPositionsCalculator Pixelation { get; private set; }

        public Vector3 CameraEulerAngles
        {
            get { return transform.localEulerAngles; }
        }

        private CameraOperatorComponent _cameraOperatorComponent;
        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiStore>();
            Pixelation = new PixelatedPositionsCalculator(_guiStore, this);
            _cameraOperatorComponent = GetComponentInChildren<CameraOperatorComponent>();
        }
        
        private void Start()
        {
        }

        public Vector3 PixelatedFocusPoint
        {
            get { return Pixelation.GetClosestPixelatedPosition(_cameraOperatorComponent.FocusPoint); }
        }

        public Vector3 FocusPoint
        {
            get { return _cameraOperatorComponent.FocusPoint; }
        }

        public Vector3 CameraPlaneOffset
        {
            get
            {
                return transform.InverseTransformPoint(_cameraOperatorComponent.FocusPoint) -
                       transform.InverseTransformPoint(PixelatedFocusPoint);
            }
        }
        
        private void Update()
        {
        }
    }
}