using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds>
    {
        private readonly NextStateBuilder<TStateIds> _nextStateBuilder;
        protected readonly IMap Map;
        protected readonly IUnitControl Unit;
        protected readonly MovementHelper MovementHelper;
        protected readonly TargetingHelper TargetingHelper;

        protected CognitionState(TStateIds id, MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map)
        {
            Id = id;
            Unit = unit;
            Map = map;
            MovementHelper = movementHelper;
            TargetingHelper = targetingHelper;
            _nextStateBuilder = new NextStateBuilder<TStateIds>(this);
        }

        public TStateIds Id { get; private set; }

        public abstract CognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        public virtual void OnGUI()
        {
        }

        public bool IsDisposed { get; private set; }
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