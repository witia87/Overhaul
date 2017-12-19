using Assets.Units;
using Assets.Units.Modules;

namespace Assets.Gui.Player
{
    public interface IPlayerStore
    {
        HeadModule PlayerHeadModule { get; }
    }
}