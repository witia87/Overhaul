﻿using Assets.Cognitions.Maps.Nodes;

namespace Assets.Cognitions.Maps.PathFinders
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