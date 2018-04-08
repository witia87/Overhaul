using Assets.Modules.Guns;
using Assets.Modules.Units.Vision;
using UnityEngine;

namespace Assets.Modules.Units
{
    public interface IUnit
    {
        IUnitControl Control { get; }
        IGun Gun { get; }
        IVisionSensor Vision { get; }

        Vector3 Position { get; }
        Vector3 LogicPosition { get; }
        Vector3 Velocity { get; }

        FractionId Fraction { get; }
    }
}