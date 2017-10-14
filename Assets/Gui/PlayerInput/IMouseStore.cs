using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public interface IMouseStore
    {
        Vector2 MousePositionInBoardSpace { get; }
        bool IsMousePressed { get; }
    }
}