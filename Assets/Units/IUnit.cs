using Assets.Units.Guns;
using Assets.Units.Modules.Coordinator.Vision;
using UnityEngine;

namespace Assets.Units
{
    public interface IUnit
    {
        IVisionSensor Vision { get; }
        IUnitControl Control { get; }
        IGun Gun { get; }

        Vector3 Position { get; }
        Vector3 LogicPosition { get; }
        Vector3 Velocity { get; }

        FractionId Fraction { get; }
    }
}