using UnityEngine;

namespace Assets.NewUnits
{
    public interface INewTargetingModuleParameters
    {
        Vector3 ModuleUp { get; }
        Vector3 ModuleForward { get; }
        Vector3 ModuleRigth { get; }
        float CrouchLevel { get; }
    }
}