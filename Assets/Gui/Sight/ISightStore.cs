using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gui.Sight
{
    public interface ISightStore
    {
        List<Vector2> GetSightPolygon(Vector3 center);
        void RegisterWallRectangle(Vector2[] rectangle);
    }
}