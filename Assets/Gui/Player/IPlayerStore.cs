using Assets.Units;

namespace Assets.Gui.Player
{
    public interface IPlayerStore
    {
        IUnitControl PlayerUnitControl { get; }
    }
}