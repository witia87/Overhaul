using UnityEngine;

namespace Assets.Modules.Units
{
    public interface IUnitControlParameters
    {
        bool IsSetToMove { get; }
        Vector3 MoveScaledLogicDirection { get; }
        Vector3 LookLogicDirection { get; }
        Vector3 AimAtDirection { get; }
        bool IsSetToFire { get; }

        bool IsSetToCrouch { get; }
    }
}