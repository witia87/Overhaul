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
            int nodesToBeBypassedCount = FindClearRectangleIndex(path);
            if (nodesToBeBypassedCount > 0)
            {
                path.RemoveRange(0, nodesToBeBypassedCount);
            }

            if (!_map.ArePositionsOnTheSameTile(_unit.Position, path[0]))
            {
                _unit.Movement.GoTo(path[0]);
            }
            else
            {
                _unit.Movement.StopMoving();
            }
        }

        private int FindClearRectangleIndex(List<Vector3> path)
        {
            var nodesToBeBypassedCount = 1;
            while (nodesToBeBypassedCount < path.Count &&
                   _map.IsRectangleClear(_unit.Position, path[nodesToBeBypassedCount - 1]))
            {
                nodesToBeBypassedCount++;
            }
            return nodesToBeBypassedCount - 1;
        }
    }
}