using UnityEngine;

namespace Assets.NewUnits
{
    public interface IUnitControlParameters
    {
        bool IsMoveGlobalDirectionSet { get; }
        Vector3 MoveGlobalDirection { get; }

        bool IsLookGlobalDirectionSet { get; }
        Vector3 LookGlobalDirection { get; }
    }
}