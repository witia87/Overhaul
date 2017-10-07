using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiStore : MonoBehaviour
    {
        public int OutlineSize = 1;

        public int PixelizationSize = 4;

        public float PixelsPerUnitInCameraSpace = 8;

        public int BoardPixelWidth
        {
            get { return Screen.width / PixelizationSize * 2; }
        }

        public int BoardPixelHeight
        {
            get { return Screen.height / PixelizationSize * 2; }
        }
        
        public CameraComponent _cameraComponent;
        [SerializeField] private Camera _guiCamera;
        private void Update()
        {
            // Commenting the code below will tur off the camera smoothing and restore the full low pixel display effect.
            var offset = new Vector3(
                _cameraComponent.CameraPlaneOffset.x * PixelsPerUnitInCameraSpace,
                _cameraComponent.CameraPlaneOffset.y * PixelsPerUnitInCameraSpace,
                0);
            _guiCamera.transform.localPosition = offset;
        }
    }
}