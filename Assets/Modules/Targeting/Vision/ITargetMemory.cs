using System;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public interface ITargetMemory
    {
        float LastSeenTime { get; }
        Vector3 LastSeenPosition { get; }
        Vector3 LastSeenVelocity{ get; }
    }
}