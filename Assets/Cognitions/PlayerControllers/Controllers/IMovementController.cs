using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public interface IMovementController
    {
        bool IsMovementPresent { get; }
        Vector3 MovementVector { get; }

        bool IsJumpPressed { get; }
    }
}