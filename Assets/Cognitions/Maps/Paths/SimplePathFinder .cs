using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGrids;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using UnityEngine;

namespace Assets.Cognitions.Maps.Paths
{
    public class SimplePathFinder
    {
        private readonly MapGrid _map;
        private ISortedStack _nodesToVisit;
        private int _stepsLimit;
        private INode[,] _visitedNodes;

        public SimplePathFinder(MapGrid map, int stepsLimit)
        {
            _map = map;
            _stepsLimit = stepsLimit;
        }

        /// <summary>
        ///     Returns the path from start to end.
        ///     Start is never on the list, so if start and end are on the same tile, then it returns empty list;
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
            var stepsCalculated = 0;
            while (stepsCalculated < _stepsLimit &&
                   _nodesToVisit.Count > 0 && _nodesToVisit.Peek() != endNode)
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

                stepsCalculated++;
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