using UnityEngine;

namespace Assets.Modules.Targeting
{
    public interface ITargetingParameters
    {
        Vector3 TargetingDirection { get; }


        /// <summary>
        ///     A value from [0.5..1] range,
        ///     where 0.5 means full crouch, and 1 means that module is straightened.
        /// </summary>
        float CrouchLevel { get; }
    }
}