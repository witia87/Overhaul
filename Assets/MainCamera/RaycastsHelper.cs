using Assets.Utilities;
using UnityEngine;

namespace Assets.MainCamera
{
    public class RaycastsHelper
    {
        private readonly Camera _camera;
        private readonly int _floorLayerMask = Layers.Floor;

        private readonly int _targetLayerMask = Layers.Map | Layers.MapTransparent
                                                | Layers.Structure | Layers.StructureTransparent
                                                | Layers.Environment | Layers.EnvironmentTransparent
                                                | Layers.Organism | Layers.OrganismTransparent;

        public RaycastsHelper(Camera camera)
        {
            _camera = camera;
        }

        public bool ScreenPointToFloorRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _floorLayerMask);
        }

        public bool ScreenPointToRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _targetLayerMask);
        }
    }
}