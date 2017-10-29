using UnityEngine;

namespace Assets.Vision
{
    public interface ITarget : ITargetMemory
    {
        bool IsVisible { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        Vector3 Center { get; }
    }
}