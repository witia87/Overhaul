using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Modules
{
    public interface IUnitControl
    {
        IMovementControl Movement { get; }
        ITargetingControl Targeting { get; }

        GameObject gameObject { get; }
        Rigidbody Rigidbody { get; }
    }
}