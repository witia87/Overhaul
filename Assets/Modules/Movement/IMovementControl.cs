using UnityEngine;

namespace Assets.Modules.Movement
{
    public interface IMovementControl : IMovementModuleParameters, IModuleControl
    {
        void Move(Vector3 globalDirection);
        void GoTo(Vector3 position);

        void Jump(Vector3 direction);

        void StopMoving();
    }
}