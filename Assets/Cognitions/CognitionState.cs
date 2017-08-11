using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds>
    {
        private readonly NextStateBuilder<TStateIds> _nextStateBuilder;
        protected readonly IMap Map;
        protected readonly MovementHelper MovementHelper;
        protected readonly TargetingHelper TargetingHelper;
        protected readonly IUnitControl Unit;

        protected CognitionState(TStateIds id, MovementHelper movementHelper, TargetingHelper targetingHelper,
            IUnitControl unit, IMap map)
        {
            Id = id;
            Unit = unit;
            Map = map;
            MovementHelper = movementHelper;
            TargetingHelper = targetingHelper;
            _nextStateBuilder = new NextStateBuilder<TStateIds>(this);
        }

        public TStateIds Id { get; private set; }

        public bool IsDisposed { get; private set; }

        public abstract CognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        public virtual void OnGUI()
        {
        }

        protected IExtendedStateBuilder<TStateIds> DisposeCurrent()
        {
            IsDisposed = true;
            return _nextStateBuilder;
        }

        protected IStateBuilder<TStateIds> RememberCurrent()
        {
            return _nextStateBuilder;
        }
    }
}