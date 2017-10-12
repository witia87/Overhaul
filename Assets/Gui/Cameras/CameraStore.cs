using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraStore : MonoBehaviour
    {
        public PixelatedPositionsCalculator Pixelation { get; private set; }

        public float Rescale = 8;

        public Vector3 CameraEulerAngles
        {
            get { return transform.localEulerAngles; }
        }

        private CameraOperatorComponent _cameraOperatorComponent;
        private void Awake()
        {
            Pixelation = new PixelatedPositionsCalculator(this);
            _cameraOperatorComponent = GetComponentInChildren<CameraOperatorComponent>();
            transform.localScale = new Vector3(1/Rescale, 1/Rescale, 1);
        }

        public Vector3 CameraPositionInBoardSpace
        {
            get { return _cameraOperatorComponent.FocusPointInBoardSpace; }
        }

        public Vector3 PixelatedCameraPositionInBoardSpace
        {
            get
            {
                return new Vector3(
                    Mathf.Round(_cameraOperatorComponent.FocusPointInBoardSpace.x),
                    Mathf.Round(_cameraOperatorComponent.FocusPointInBoardSpace.y),
                    0);
            }
        }
    }
}