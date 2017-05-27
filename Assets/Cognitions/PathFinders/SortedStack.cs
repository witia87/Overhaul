using System.Collections.Generic;
using Assets.Map.Nodes;
using UnityEngine;

namespace Assets.Cognitions.PathFinders
{
    public class SortedStack : ISortedStack
    {
        private readonly List<float> _distances;
        private readonly List<INode> _nodes;
        private readonly Vector3 _target;

        public SortedStack(Vector3 target)
        {
            _target = target;
            _nodes = new List<INode>();
            _distances = new List<float>();
        }

        public void Emplace(INode node)
        {
            var currentDistance = -(node.Position - _target).magnitude;
            // The zero-based index of item in the sorted List<T>, if item is found; otherwise, a negative number
            // that is the bitwise complement of the index of the next element that is larger than item or, 
            // if there is no larger element, the bitwise complement of Count.
            var index = _distances.BinarySearch(currentDistance);
            if (index < 0)
            {
                index = ~index;
            }

            _distances.Insert(index, currentDistance);
            _nodes.Insert(index, node);
        }

        public INode Pop()
        {
            var node = _nodes[_nodes.Count - 1];
            _nodes.RemoveAt(_nodes.Count - 1);
            _distances.RemoveAt(_distances.Count - 1);
            return node;
        }

        public INode Peek()
        {
            return _nodes[_nodes.Count - 1];
        }

        public int Count
        {
            get { return _nodes.Count; }
        }

        public bool IsEmpty
        {
            get { return _nodes.Count == 0; }
        }
    }
}