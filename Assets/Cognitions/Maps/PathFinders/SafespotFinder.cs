using System.Collections.Generic;
using Assets.Cognitions.Maps.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.PathFinders
{
    public class SafespotFinder
    {
        private readonly Map _map;
        private List<INode> _nodesToVisit;
        private INode[,] _visitedNodes;

        public SafespotFinder(Map map)
        {
            _map = map;
            Reset();
        }

        public List<Vector3> FindClosestSafespot(Vector3 position)
        {
            Reset();
            INode startNode;
            _map.TryGetNode(position, out startNode);

            _nodesToVisit.Add(startNode);
            while (_nodesToVisit.Count > 0 &&
                   _map.IsPositionDangorous(_nodesToVisit[_nodesToVisit.Count - 1].Position))
            {
                var currentNode = _nodesToVisit[0];
                _nodesToVisit.RemoveAt(0);
                var enumerator = currentNode.GetRandomizedNeighborsEnumerator();
                while (enumerator.MoveNext())
                {
                    var node = enumerator.Current;
                    if (_visitedNodes[node.z, node.x] == null)
                    {
                        _nodesToVisit.Add(node);
                        _visitedNodes[node.z, node.x] = currentNode;
                    }
                }
            }

            if (_nodesToVisit.Count > 0)
            {
                return RecreatePath(startNode, _nodesToVisit[_nodesToVisit.Count - 1]);
            }

            return new List<Vector3>();
        }

        private List<Vector3> RecreatePath(INode startNode, INode endNode)
        {
            var list = new List<Vector3>();
            var currentNode = endNode;
            while (currentNode != startNode)
            {
                list.Add(currentNode.Position);
                currentNode = _visitedNodes[currentNode.z, currentNode.x];
            }

            list.Reverse();

            return list;
        }

        private void Reset()
        {
            _nodesToVisit = new List<INode>();
            _visitedNodes = new INode[_map.Grid.GetLength(0), _map.Grid.GetLength(1)];
        }
    }
}