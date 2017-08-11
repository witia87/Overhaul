using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Modules
{
    public interface IUnitControl
    {
        IMovementControl Movement { get; }
        ITargetingControl Targeting { get; }

        Vector3 Position { get; }
        Rigidbody Rigidbody { get; }
    }
}