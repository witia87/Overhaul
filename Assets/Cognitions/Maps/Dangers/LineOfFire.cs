using Assets.Cognitions.Maps.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.Dangers
{
    public class LineOfFire
    {
        private readonly BaseNode[,] _baseGrid;
        private readonly Vector3 _direction;
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
            var x = Mathf.FloorToInt(position.x);
            var z = Mathf.FloorToInt(position.z);
            node = _baseGrid[z, x];
            return node != null;
        }
    }
}