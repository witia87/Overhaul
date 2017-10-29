using System.Collections.Generic;
using Assets.Vision;
using UnityEngine;

namespace Assets.Units.Heads.Vision
{
    public interface IVisionSensor
    {
        Vector3 SightDirection { get; }
        Vector3 SightPosition { get; }
        int VisibleTargetsCount { get; }
        ITarget GetClosestTarget();
        List<Vector3> GetThreeClosestDirections();
    }
}