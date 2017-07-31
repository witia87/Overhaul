using System.Collections.Generic;
using Assets.Maps.Nodes;
using UnityEngine;

namespace Assets.Maps.PathFinders
{
    public class SimplePathFinder
    {
        private readonly Map _map;
        private ISortedStack _nodesToVisit;
        private INode[,] _visitedNodes;

        public SimplePathFinder(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Returns the path from start to end.
        /// Start is never on the list, so if start and end are on the same tile, then it returns empty list;
        /// </summary>
        public List<Vector3> FindPath(Vector3 start, Vector3 end)
        {
            Reset(end);
            INode startNode;
            _map.TryGetNode(start, out startNode);
            INode endNode;
            _map.TryGetNode(end, out endNode);

            _nodesToVisit.Emplace(startNode);
            var closestNode = startNode;
            while (_nodesToVisit.Count > 0 && _nodesToVisit.Peek() != endNode)
            {
                var currentNode = _nodesToVisit.Pop();
                var enumerator = currentNode.GetSimpleNeighborsEnumerator();
                while (enumerator.MoveNext())
                {
                    var node = enumerator.Current;
                    if (_visitedNodes[node.z, node.x] == null)
                    {
                        _nodesToVisit.Emplace(node);
                        _visitedNodes[node.z, node.x] = currentNode;
                        if ((end - node.Position).magnitude <
                            (end - closestNode.Position).magnitude)
                        {
                            closestNode = node;
                        }
                    }
                }
            }

            return RecreatePath(startNode, closestNode);
        }

        private List<Vector3> RecreatePath(INode startNode, INode endNode)
        {
            var list = new List<Vector3>();
            list.Add(endNode.Position);
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                currentNode = _visitedNodes[currentNode.z, currentNode.x];
                list.Add(currentNode.Position);
            }

            list.Reverse();

            return list;
        }

        private void Reset(Vector3 target)
        {
            _nodesToVisit = new SortedStack(target);
            _visitedNodes = new INode[_map.Grid.GetLength(0), _map.Grid.GetLength(1)];
        }
    }
}