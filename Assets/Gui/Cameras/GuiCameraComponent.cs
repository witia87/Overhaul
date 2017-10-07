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
            transform.parent = transform;
            Camera.cameraType = CameraType.Preview;
            Camera.orthographic = true;
            Camera.transform.localPosition = Vector3.zero;
            Camera.transform.localEulerAngles = Vector3.zero;
            Camera.orthographicSize = Screen.height / (float)_guiStore.PixelizationSize / 2;
            Camera.aspect = Screen.width / (float)Screen.height;
            Camera.SetupCurrent(Camera);
        }
    }
}