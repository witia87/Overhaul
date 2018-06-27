using Assets.Environment.Guns;
using Assets.Environment.Units.Vision;
using UnityEngine;

namespace Assets.Environment.Units
{
    public interface IUnit
    {
        int UnitScale { get; }
        IUnitControl Control { get; }
        IGun Gun { get; }
        IVisionSensor Vision { get; }

        Vector3 Position { get; }
        Vector3 LogicPosition { get; }
        Vector3 Velocity { get; }

        FractionId Fraction { get; }
    }
}