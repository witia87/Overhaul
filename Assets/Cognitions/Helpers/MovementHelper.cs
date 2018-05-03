using System.Collections.Generic;
using Assets.Cognitions.Maps;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Helpers
{
    public class MovementHelper
    {
        private readonly IMap _map;
        private readonly IUnit _unit;

        public MovementHelper(IUnit unit, IMap map)
        {
            _unit = unit;
            _map = map;
        }

        public void ManageMovingAlongThePath(List<Vector3> path)
        {
            var nodesToBeBypassedCount = FindClearRectangleIndex(path);
            if (nodesToBeBypassedCount > 0)
            {
                path.RemoveRange(0, nodesToBeBypassedCount);
            }

            if (!_map.ArePositionsOnTheSameTile(_unit.LogicPosition, path[0]))
            {
                _unit.Control.Move((path[0] - _unit.LogicPosition).normalized);
            }
        }

        private int FindClearRectangleIndex(List<Vector3> path)
        {
            var nodesToBeBypassedCount = 1;
            while (nodesToBeBypassedCount < path.Count &&
                   _map.IsRectangleClear(_unit.LogicPosition, path[nodesToBeBypassedCount - 1]))
            {
                nodesToBeBypassedCount++;
            }

            return nodesToBeBypassedCount - 1;
        }
    }
}