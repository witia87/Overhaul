using Assets.Maps.Nodes;

namespace Assets.Maps.PathFinders
{
    public interface ISortedStack
    {
        int Count { get; }
        bool IsEmpty { get; }
        void Emplace(INode node);
        INode Pop();
        INode Peek();
    }
}