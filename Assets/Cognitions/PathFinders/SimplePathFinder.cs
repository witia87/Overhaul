using System.Collections.Generic;
using Assets.Map;
using Assets.Map.Nodes;
using UnityEngine;

namespace Assets.Cognitions.PathFinders
{
    public class SimplePathFinder : IPathFinder
    {
        private readonly IMapStore _mapStore;
        private readonly int _scale;
        private ISortedStack _nodesToVisit;
        private INode[,] _visitedNodes;

        public SimplePathFinder(IMapStore mapStore, int scale)
        {
            _mapStore = mapStore;
            _scale = scale;
        }

        public List<INode> FindPath(Vector3 start, Vector3 end)
        {
            Reset(end);
            INode startNode;
            _mapStore.TryGetNode(start, _scale, out startNode);
            INode endNode;
            _mapStore.TryGetNode(end, _scale, out endNode);

            _nodesToVisit.Emplace(startNode);
            var closestNode = startNode;
            while (_nodesToVisit.Count > 0 && _nodesToVisit.Peek() != endNode)
            {
                var currentNode = _nodesToVisit.Pop();
                var enumerator = currentNode.GetRandomizedNeighborsEnumerator();
                while (enumerator.MoveNext())
                {
                    var node = enumerator.Current;
                    if (_visitedNodes[node.z, node.x] == null)
                    {
                        _nodesToVisit.Emplace(node);
                        _visitedNodes[node.z, node.x] = currentNode;
                        if ((endNode.Position - node.Position).magnitude <
                            (closestNode.Position - node.Position).magnitude)
                        {
                            closestNode = node;
                        }
                    }
                }
            }

            return RecreatePath(startNode, closestNode);
        }

        private List<INode> RecreatePath(INode startNode, INode endNode)
        {
            var list = new List<INode>();
            list.Add(endNode);
            while (list[list.Count - 1] != startNode)
            {
                list.Add(_visitedNodes[list[list.Count - 1].z, list[list.Count - 1].x]);
            }
            list.Reverse();

            return list;
        }

        private void Reset(Vector3 target)
        {
            _nodesToVisit = new SortedStack(target);
            _visitedNodes = new INode[_mapStore.Grid[_scale].GetLength(0),
                _mapStore.Grid[_scale].GetLength(1)];
        }
    }
}