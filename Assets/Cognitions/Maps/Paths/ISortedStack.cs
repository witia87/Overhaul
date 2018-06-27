using Assets.Cognitions.Maps.MapGrids.Nodes;

namespace Assets.Cognitions.Maps.Paths
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