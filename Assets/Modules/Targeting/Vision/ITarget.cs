using System;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public interface ITarget
    {
        bool IsVisible { get; }
        Vector3 LastSeenPosition { get; }
        Vector3 LastSeenMovementDirection { get; }
        Vector3 Center { get; }
        Vector3 MovementDirectionPrediction { get; }
    }
}