using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class OutlineCameraComponent : MonoBehaviour
    {
        private CameraComponent _cameraStore;
        private IGuiStore _guiStore;

        public Camera OutlineCamera { get; private set; }

        private void Awake()
        {
            _cameraStore = FindObjectOfType<CameraComponent>();
            _guiStore = FindObjectOfType<GuiComponent>();

            OutlineCamera = GetComponent<Camera>();
            OutlineCamera.cameraType = CameraType.Game;
            OutlineCamera.orthographic = true;
            
        }

        public void Initialize()
        {
            OutlineCamera.orthographicSize = _cameraStore.MainCamera.orthographicSize;
            OutlineCamera.aspect = _cameraStore.MainCamera.aspect;
            OutlineCamera.targetTexture = _guiStore.OutlineTexture;
            //OutlineCamera.SetReplacementShader(Resources.Load("Materials/Shaders/OutlineBoard", typeof(Shader)) as Shader, "OutlineType");
            OutlineCamera.transform.localEulerAngles = Vector3.zero;
            OutlineCamera.transform.localPosition = Vector3.zero;
            OutlineCamera.transform.localScale = new Vector3(1,1,1);
        }
        
    }
}