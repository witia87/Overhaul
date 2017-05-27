using UnityEngine;

namespace Assets.Modules.Movement
{
    public interface IVehicleMovementControl
    {
        Vector3 UnitDirection { get; }
        Vector3 MovementDirection { get; }
        MovementType MovementType { get; }

        void MoveForward(float speed);
        void MoveBackward(float speed);

        void TurnLeft();
        void TurnRight();
        void TurnFrontTowards(Vector3 globalDirection);
        void TurnBackTowards(Vector3 globalDirection);
        void LookAtFront(Vector3 position);
        void LookAtBack(Vector3 position);

        void Jump(Vector3 localDirection);
        void JumpTowards(Vector3 direction);

        void StopMoving();
        void StopTurning();

    }
}