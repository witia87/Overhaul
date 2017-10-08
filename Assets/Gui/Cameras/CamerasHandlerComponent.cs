using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CamerasHandlerComponent : MonoBehaviour
    {
        private CameraStore _cameraStore;
        
        private void Awake()
        {
            _cameraStore = FindObjectOfType<CameraStore>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.transform.position = _cameraStore.PixelatedFocusPoint;
        }
    }
}