using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public interface IKeyboardStore
    {
        bool IsMovementPresent { get; }
        Vector3 MovementVector { get; }
        bool IsJumpPressed { get; }
        bool IsCrouchPressed { get; }
    }
}