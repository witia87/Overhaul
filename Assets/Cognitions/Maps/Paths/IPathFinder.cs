﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cognitions.Maps.Paths
{
    public interface IPathFinder
    {
        List<Vector3> FindPath(Vector3 start, Vector3 end);
        List<Vector3> FindSafespot(Vector3 start);
    }
}