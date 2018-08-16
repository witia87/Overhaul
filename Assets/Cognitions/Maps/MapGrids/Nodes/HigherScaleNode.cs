using Assets.Cognitions.Maps.MapGraphs.Rooms;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGrids.Nodes
{
    public class HigherScaleNode : INode
    {
        private readonly BaseNode[,] _baseGrid;
        private readonly INode[,] _grid;
        private readonly IRoom _room;
        private readonly int _scale;

        public HigherScaleNode(BaseNode[,] baseGrid, INode[,] grid, int x, int z, int scale,
            Vector3 position)
        {
            _baseGrid = baseGrid;
            _grid = grid;
            _room = baseGrid[z,x].Room;
            _scale = scale;
            this.x = x;
            this.z = z;
            Position = position;
        }

        public int x { get; private set; }
        public int z { get; private set; }
        public Vector3 Position { get; private set; }

        public bool IsCovered(Vector3 direction)
        {
            for (var _z = z;
                _z <= Mathf.Min(_baseGrid.GetLength(0) - 1, z + _scale);
                _z++)
            for (var _x = x;
                _x <= Mathf.Min(_baseGrid.GetLength(1) - 1, x + _scale);
                _x++)
                if (!_baseGrid[_z, _x].IsCovered(direction))
                    return false;

            return true;
        }

        public bool IsDangerous
        {
            get
            {
                for (var _z = z;
                    _z <= Mathf.Min(_baseGrid.GetLength(0) - 1, z + _scale);
                    _z++)
                for (var _x = x;
                    _x <= Mathf.Min(_baseGrid.GetLength(1) - 1, x + _scale);
                    _x++)
                    if (_baseGrid[_z, _x].IsDangerous)
                        return true;

                return false;
            }
        }


        public IRoom Room
        {
            get { return _room; }
        }
    }
}