using Assets.Modules.Movement;
using Assets.Modules.Turrets;
using UnityEngine;

namespace Assets.Modules
{
    public interface IModulesNetwork
    {
        bool IsMovementControlMounted { get; }
        bool IsTurretControlMounted { get; }
        IMovementControl MovementControl { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        ITurretControl TurretControl { get; }
    }
}