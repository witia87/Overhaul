using UnityEngine;

namespace Assets.Cognitions.Players.Controllers
{
    public interface IMovementController
    {
        bool IsMovementPresent { get; }
        Vector3 MovementVector { get; }

        bool IsJumpPressed { get; }
    }
}