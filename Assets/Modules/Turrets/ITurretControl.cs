using Assets.Modules.Vision;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public interface ITurretControl : IVisionSensor
    {
        Vector3 SightDirection { get; }
        Vector3 SightPosition { get; }

        float CooldownTime { get; }
        float CooldownTimeLeft { get; }
        void TurnTowards(Vector3 direction);
        void LookAt(Vector3 point);
        void Fire();
    }
}