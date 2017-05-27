using Assets.Modules.Vision;
using UnityEngine;

namespace Assets.Modules.Turrets
{
    public interface ITurretControl : IVisionSensor, ITurretParameters
    {
        float CooldownTime { get; }
        float CooldownTimeLeft { get; }
        void TurnTowards(Vector3 direction);
        void LookAt(Vector3 point);
        void Fire();
    }
}