using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraComponent : GuiComponent
    {
        public Camera Camera { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Camera = GetComponent<Camera>();
        }

        public void Start()
        {
            Camera.orthographicSize = BoardStore.BoardTextureHeight / CameraStore.Rescale / 2;
            Camera.aspect = BoardStore.BoardTextureWidth / (float) BoardStore.BoardTextureHeight;

            Camera.targetTexture.width = BoardStore.BoardTextureWidth;
            Camera.targetTexture.height = BoardStore.BoardTextureHeight;
        }
    }
}