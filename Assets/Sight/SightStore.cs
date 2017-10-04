using System.Collections.Generic;
using Assets.Units;
using UnityEngine;

namespace Assets.Sight
{
    public class SightStore : MonoBehaviour
    {
        public Unit Unit;
        private VisibilityComputer _visibilityComputer;
        private void Awake()
        {
            _visibilityComputer = new VisibilityComputer();
            _visibilityComputer.Radius = 30;

        }

        public Vector2 Center {  get { return _visibilityComputer.Origin; } }
        public List<Vector2> GetSightPolygon()
        {
            _visibilityComputer.Origin = new Vector2(Unit.Position.x, Unit.Position.z);
            return _visibilityComputer.Compute();
        }

        public void RegisterPolygon(Vector2[] polygon)
        {
            for (int i = 0; i < polygon.Length; i++)
            {
                _visibilityComputer.AddSegment(polygon[i], polygon[(i+1)%polygon.Length]);
            }
        }
    }
}