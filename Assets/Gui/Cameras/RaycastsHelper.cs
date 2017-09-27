using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class RaycastsHelper
    {
        private readonly Camera _camera;
        private readonly LayerMask _emptyTargetingLayerMask;
        private readonly LayerMask _environmentLayerMask;

        private readonly LayerMask _targetLayerMask;

        public RaycastsHelper(Camera camera, LayerMask emptyTargetingLayerMask, LayerMask targetLayerMask, LayerMask environmentLayerMask)
        {
            _camera = camera;
            _emptyTargetingLayerMask = emptyTargetingLayerMask;
            _targetLayerMask = targetLayerMask;
            _environmentLayerMask = environmentLayerMask;
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

        public bool ScreenPointToRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _targetLayerMask);
        }
    }
}