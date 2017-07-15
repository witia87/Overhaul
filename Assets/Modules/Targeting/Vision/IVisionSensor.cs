using System.Collections.Generic;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public interface IVisionSensor
    {
        Vector3 SightPosition { get; }
        List<GameObject> VisibleGameObjects { get; }
        List<Module> VisibleModules { get; }
    }
}