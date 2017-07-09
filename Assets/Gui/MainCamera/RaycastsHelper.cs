﻿using Assets.Utilities;
using UnityEngine;

namespace Assets.Gui.MainCamera
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
            var viewportPosition = new Vector2(screenPosition.x/Screen.width, screenPosition.y/Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _floorLayerMask);
        }

        public bool ScreenPointToRay(Vector2 screenPosition, out RaycastHit mouseHit)
        {
            var viewportPosition = new Vector2(screenPosition.x/Screen.width, screenPosition.y/Screen.height);
            var ray = _camera.ViewportPointToRay(viewportPosition);
            return Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _targetLayerMask);
        }
    }
}