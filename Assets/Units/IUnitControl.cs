using Assets.Units.Guns;
using Assets.Units.Vision;
using UnityEngine;

namespace Assets.Units
{
    public interface IUnitControl
    {
        Vector3 Position { get; }
        Vector3 Center { get; }
        Vector3 Velocity { get; }

        IVisionSensor Vision { get; }
        IGunControl Gun { get; }

        /// <summary>
        ///     Makes unit perform actions in order to look in the desired direction.
        /// </summary>
        /// <param name="globalDirection">Vector3 needs to be normalized and has y=0</param>
        void LookTowards(Vector3 globalDirection);

        void LookAt(Vector3 globalPoint);
        void Move(Vector3 logicDirection, float speedModifier);
        void Crouch();
        void Jump(Vector3 globalDirection, float jumpForceModifier);
    }
}