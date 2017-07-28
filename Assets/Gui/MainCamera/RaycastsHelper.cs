using UnityEngine;

namespace Assets.Gui.MainCamera
{
    public class RaycastsHelper
    {
        private readonly Camera _camera;
        private LayerMask _floorLayerMask;

        private LayerMask _targetLayerMask;

        public RaycastsHelper(Camera camera, LayerMask floorLayerMask, LayerMask targetLayerMask)
        {
            _camera = camera;
            _floorLayerMask = floorLayerMask;
            _targetLayerMask = targetLayerMask;
        }

        public bool ScreenPointToFloorRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _floorLayerMask);
        }

        public bool ScreenPointToRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _targetLayerMask);
        }
    }
}