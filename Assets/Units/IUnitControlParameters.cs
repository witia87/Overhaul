using UnityEngine;

namespace Assets.Units
{
    public interface IUnitControlParameters
    {
        bool IsSetToMove { get; }
        Vector3 MoveScaledLogicDirection { get; }
        Vector3 LookLogicDirection { get; }
        Vector3 AimAtDirection { get; }
        Vector3 ShouldFire { get; }

        bool IsSetToCrouch { get; }
    }
}