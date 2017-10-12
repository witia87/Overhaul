using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class RaycastingCameraComponent: MonoBehaviour
    {
        [SerializeField] private LayerMask _emptyTargetingLayerMask;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private LayerMask _environmentLayerMask;

        BoardStore _guiStore;
        CameraStore _cameraStore;
        private Camera _camera;
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _guiStore = FindObjectOfType<BoardStore>();
            _cameraStore = FindObjectOfType<CameraStore>();
        }

        /*private void Start()
        {
            _camera.orthographicSize = _guiStore.ViewOrtographicSize;
            _camera.aspect = _guiStore.ViewOrtographicAspect;
        }

        public bool ScreenPointToEmptyTargetingRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _emptyTargetingLayerMask);
        }

        public bool ScreenPointToEnvironmentRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _environmentLayerMask);
        }

        public bool ScreenPointToModuleRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _targetLayerMask);
        }

        private void Update()
        {
            this.transform.position = _cameraStore.FocusPoint;
        }*/
    }
}