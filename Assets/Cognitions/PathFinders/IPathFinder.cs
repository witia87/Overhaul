using System.Collections.Generic;
using Assets.Map.Nodes;
using UnityEngine;

namespace Assets.Cognitions.PathFinders
{
    public interface IPathFinder
    {
        List<INode> FindPath(Vector3 start, Vector3 end);
    }
}