using System.Collections.Generic;
using Assets.Gui.Player;
using Assets.Gui.Sight.Polygons;
using Assets.Gui.Sight.Visibility;
using UnityEngine;

namespace Assets.Gui.Sight
{
    public class SightStore : GuiStore, ISightStore
    {
        private PlayerStore _playerStore;
        private readonly PolygonsOptimizer _polygonsOptimizer = new PolygonsOptimizer();
        private VisibilityComputer _visibilityComputer;

        public Vector2 Center
        {
            get { return _visibilityComputer.Origin; }
        }

        public List<Vector2> GetSightPolygon(Vector3 center)
        {
            _visibilityComputer.Origin = new Vector2(center.x, center.z);
            return _visibilityComputer.Compute();
        }

        public void RegisterWallRectangle(Vector2[] rectangle)
        {
            _polygonsOptimizer.RegisterRectangle(rectangle);
        }

        private void Awake()
        {
            _playerStore = FindObjectOfType<PlayerStore>();
            _visibilityComputer = new VisibilityComputer();
            _visibilityComputer.Radius = 30;
        }

        private void Start()
        {
            var polygons = _polygonsOptimizer.GetOptimizedPolygons();
            foreach (var polygon in polygons)
            {
                UploadWallRectangle(polygon);
            }

            _visibilityComputer.AddSegment(new Vector2(1, 1), new Vector2(39, 1));
            _visibilityComputer.AddSegment(new Vector2(39, 1), new Vector2(39, 39));
            _visibilityComputer.AddSegment(new Vector2(39, 39), new Vector2(1, 39));
            _visibilityComputer.AddSegment(new Vector2(1, 39), new Vector2(1, 1));
        }

        private void UploadWallRectangle(Vector2[] polygon)
        {
            for (var i = 0; i < polygon.Length; i++)
            {
                _visibilityComputer.AddSegment(polygon[i], polygon[(i + 1) % polygon.Length]);
            }
        }
    }
}