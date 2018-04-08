using UnityEngine;

namespace Assets.Cognitions.Vision
{
    public interface ITargetMemory
    {
        float LastSeenTime { get; }
        Vector3 LastSeenPosition { get; }
        Vector3 LastSeenVelocity { get; }
    }
}