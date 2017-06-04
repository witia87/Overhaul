using UnityEngine;

namespace Assets.Modules.Movement
{
    public interface IHumanoidMovementControl
    {
        Vector3 UnitDirection { get; }
        Vector3 MovementDirection { get; }

        void Move(Vector3 localDirection);
        void MoveTowards(Vector3 globalDirection);
        void GoTo(Vector3 position);

        void Jump(Vector3 localDirection);
        void JumpTowards(Vector3 direction);

        void StopMoving();
        void StopTurning();
    }
}