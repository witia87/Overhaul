using UnityEngine;

namespace Assets.NewUnits
{
    public interface INewMovementModuleParameters
    {
        Vector3 ModuleUp { get; }
        Vector3 ModuleForward { get; }
        Vector3 ModuleRigth { get; }
        float MovementSpeed { get; }
        float CrouchLevel { get; }
        bool IsGrounded { get; }
    }
}