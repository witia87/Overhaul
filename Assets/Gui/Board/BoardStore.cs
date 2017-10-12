using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class BoardStore : MonoBehaviour
    {
        private CameraStore _cameraStore;
        private GuiStore _guiStore;

        /// <summary>
        /// Board texture width is also the size of Board quad in the GuiSpace.
        /// </summary>
        public int BoardTextureWidth
        {
            get { return Mathf.RoundToInt(_guiStore.ScreenWidthInPixels) * 2; }
        }

        public int BoardTextureHeight
        {
            get { return Mathf.RoundToInt(_guiStore.ScreenHeightInPixels) * 2; }
        }
        
        public RaycastingCameraComponent Raycasts { get; private set; }
        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiStore>();
            _cameraStore = FindObjectOfType<CameraStore>();
            Raycasts = FindObjectOfType<RaycastingCameraComponent>();
        }
    }
}