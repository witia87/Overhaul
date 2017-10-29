using UnityEngine;

namespace Assets.Vision
{
    public interface ITargetMemory
    {
        float LastSeenTime { get; }
        Vector3 LastSeenPosition { get; }
        Vector3 LastSeenVelocity { get; }
    }
}