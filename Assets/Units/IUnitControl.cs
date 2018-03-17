using Assets.Units.Guns;
using Assets.Units.Modules.Coordinator.Vision;
using UnityEngine;

namespace Assets.Units
{
    public interface IUnitControl
    {
        Vector3 LogicPosition { get; }
        Vector3 Center { get; }
        Vector3 Velocity { get; }

        IVisionSensor Vision { get; }
        IGunControl Gun { get; }

        /// <summary>
        ///     Makes headModule perform actions in order to look in the desired direction.
        /// </summary>
        /// <param name="globalDirection">Vector3 needs to be normalized and has y=0</param>
        void LookTowards(Vector3 globalDirection);

        void LookAt(Vector3 globalPoint);
        void Move(Vector3 logicDirection, float speedModifier);
        void Crouch();
        void Jump();
    }
}