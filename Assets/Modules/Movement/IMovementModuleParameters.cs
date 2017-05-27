using UnityEngine;

namespace Assets.Modules.Movement
{
    public interface IMovementModuleParameters
    {
        Vector3 UnitDirection { get; }
        MovementType MovementType { get; }

        /// <summary>
        /// A value from [0..1] range, 
        /// where 0 means no movement, and 1 means maximal speed for this unit.
        /// </summary>
        float MovementSpeed { get; }

        bool IsGrounded { get; }
    }
}