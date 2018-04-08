using UnityEngine;

namespace Assets.Modules.Units
{
    public interface IUnitControl
    {
        /// <summary>
        ///     Sets the direction to look towards during the next FixedUpdate call.
        /// </summary>
        /// <param name="direction">Vector3 needs to be normalized</param>
        void LookTowards(Vector3 direction);

        /// <summary>
        ///     Sets the direction to move towards during the next FixedUpdate call.
        /// </summary>
        /// <param name="scaledLogicDirection">Vector3 needs to have length less then 1 and y=0</param>
        void Move(Vector3 scaledLogicDirection);

        /// <summary>
        ///     Sets unit to perform crouch (or stay that way) during the next FixedUpdate call.
        /// </summary>
        void Crouch(bool should);

        void Fire(bool should);
    }
}