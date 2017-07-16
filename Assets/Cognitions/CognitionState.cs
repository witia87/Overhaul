using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Modules;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds> : ICognitionState<TStateIds>
    {
        protected readonly IMapStore MapStore;
        protected readonly Cognition<TStateIds> ParentCognition;

        protected CognitionState(Cognition<TStateIds> parentCognition, TStateIds id)
        {
            Id = id;
            ParentCognition = parentCognition;
            MapStore = parentCognition.MapStore;
        }

        protected IPathFinder PathFinder
        {
            get { return ParentCognition.PathFinder; }
        }

        protected IUnitControl Unit
        {
            get { return ParentCognition.ConnectedUnit; }
        }

        protected int Scale
        {
            get { return ParentCognition.Scale; }
        }

        public TStateIds Id { get; private set; }

        public abstract ICognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}