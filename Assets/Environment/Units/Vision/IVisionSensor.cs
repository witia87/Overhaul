using UnityEngine;

namespace Assets.Environment.Units.Vision
{
    public interface IVisionSensor
    {
        Vector3 Direction { get; }
        Vector3 Position { get; }
        float SightLength { get; }
        float SightAngle { get; }
    }
}