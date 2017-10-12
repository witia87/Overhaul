using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui.Board
{
    public class BoardHandlerComponent: MonoBehaviour
    {
        private CameraStore _cameraStore;

        public RaycastingCameraComponent Raycasts { get; private set; }
        private void Awake()
        {
            _cameraStore = FindObjectOfType<CameraStore>();
            Raycasts = FindObjectOfType<RaycastingCameraComponent>();
        }

        private void Update()
        {
            // Commenting the code below will tur off the camera smoothing and restore the full low pixel display effect.
            var offset = _cameraStore.CameraPositionInBoardSpace - _cameraStore.PixelatedCameraPositionInBoardSpace;
            transform.localPosition = -offset;
            Debug.Log("l");
        }

    }
}