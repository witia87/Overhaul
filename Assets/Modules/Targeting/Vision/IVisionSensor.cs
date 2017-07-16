using System.Collections.Generic;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public interface IVisionSensor
    {
        Vector3 SightDirection { get; }
        Vector3 SightPosition { get; }
        List<Unit> VisibleUnits { get; }
    }
}