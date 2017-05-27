using Assets.Map.Nodes;

namespace Assets.Cognitions.PathFinders
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