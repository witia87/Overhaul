using System.Collections.Generic;
using Assets.Cores;
using UnityEngine;

namespace Assets.Modules.Vision
{
    public interface IVisionSensor
    {
        Vector3 SightPosition { get; }
        List<GameObject> VisibleGameObjects { get; }
        List<Core> VisibleCores { get; }
    }
}