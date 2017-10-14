using Assets.Gui.Board;
using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraComponent : MonoBehaviour
    {
        private BoardStore _guiStore;
        private CameraStore _cameraStore;

        public Camera Camera { get; private set; }

        private void Awake()
        {
            _guiStore = FindObjectOfType<BoardStore>();
            _cameraStore = FindObjectOfType<CameraStore>();
            Camera = GetComponent<Camera>();
        }

        public void Start()
        {
            Camera.orthographicSize = _guiStore.BoardTextureHeight / (float)_cameraStore.Rescale/ 2;
            Camera.aspect = _guiStore.BoardTextureWidth / (float)_guiStore.BoardTextureHeight;

            Camera.targetTexture.width = _guiStore.BoardTextureWidth;
            Camera.targetTexture.height = _guiStore.BoardTextureHeight;
        }
    }
}