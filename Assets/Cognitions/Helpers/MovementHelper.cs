using System.Collections.Generic;
using Assets.Maps;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cognitions.Helpers
{
    public class MovementHelper
    {
        private readonly IMap _map;
        private readonly IUnitControl _unit;

        public MovementHelper(IUnitControl unit, IMap map)
        {
            _unit = unit;
            _map = map;
        }

        public void ManageMovingAlongThePath(List<Vector3> path)
        {
            TrimPath(path);

            if (path.Count > 0)
            {
                var targetPosition = FindShortcut(path);
                _unit.Movement.GoTo(targetPosition);
            }
            else
            {
                _unit.Movement.StopMoving();
            }
        }

        private void TrimPath(List<Vector3> path)
        {
            var i = 0;
            while (i < path.Count && !_map.ArePositionsOnTheSameTile(_unit.gameObject.transform.position, path[i]))
            {
                i++;
            }
            if (i < path.Count)
            {
                path.RemoveRange(0, i + 1);
            }
        }

        private Vector3 FindShortcut(List<Vector3> path)
        {
            var shortcutLength = 0;
            while (shortcutLength < path.Count - 1 &&
                   _map.IsRectangleClear(_unit.gameObject.transform.position, path[shortcutLength]))
            {
                shortcutLength++;
            }
            return path[shortcutLength];
        }
    }
}