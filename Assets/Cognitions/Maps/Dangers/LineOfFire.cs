using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.Dangers
{
    public class LineOfFire
    {
        private readonly BaseNode[,] _baseGrid;
        private readonly Vector3 _direction;
        private readonly int _mapLength;
        private readonly int _mapWidth;
        private readonly Vector3 _startPosition;
        private readonly float _startTime;
        private readonly float _time;

        public LineOfFire(BaseNode[,] baseGrid, Vector3 startPosition, Vector3 direction, float time)
        {
            _baseGrid = baseGrid;
            _startPosition = startPosition;
            _direction = direction;
            _time = time;
            _startTime = Time.time;
            _mapLength = baseGrid.GetLength(0);
            _mapWidth = baseGrid.GetLength(1);
        }

        public bool IsFinished
        {
            get { return Time.time - _startTime > _time; }
        }

        public void Register()
        {
            BaseNode currentNode;
            var currentPosition = _startPosition;
            while (TryGetBaseNode(currentPosition, out currentNode))
            {
                currentNode.DangersCount++;
                currentPosition += _direction;
            }
        }

        public void Unregister()
        {
            BaseNode currentNode;
            var currentPosition = _startPosition;
            while (TryGetBaseNode(currentPosition, out currentNode))
            {
                currentNode.DangersCount--;
                currentPosition += _direction;
            }
        }

        private bool TryGetBaseNode(Vector3 position, out BaseNode node)
        {
            var z = Mathf.Max(0, Mathf.Min(_mapLength - 1, Mathf.FloorToInt(position.z)));
            var x = Mathf.Max(0, Mathf.Min(_mapWidth - 1, Mathf.FloorToInt(position.x)));

            node = _baseGrid[z, x];
            return node != null;
        }
    }
}