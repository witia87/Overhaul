using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using UnityEditor;
using UnityEngine;

namespace Assets.Cognitions.States
{
    public abstract class CognitionState
    {
        private readonly NextStateBuilder _nextStateBuilder;
        public readonly ComputerStateIds Id;
        protected readonly IMap Map;
        protected readonly MovementHelper MovementHelper;
        protected readonly ComputerStatesFactory StatesFactory;
        protected readonly TargetingHelper TargetingHelper;
        protected readonly IUnit Unit;
        protected IVisionObserver Vision;

        protected CognitionState(ComputerStateIds id,
            MovementHelper movementHelper, TargetingHelper targetingHelper, 
            IUnit unit, IMap map, IVisionObserver vision)
        {
            Unit = unit;
            Id = id;
            Map = map;
            Vision = vision;
            MovementHelper = movementHelper;
            TargetingHelper = targetingHelper;
            _nextStateBuilder = new NextStateBuilder(this);
            StatesFactory = new ComputerStatesFactory(map, unit, vision, movementHelper, targetingHelper);
        }

        public bool IsDisposed { get; private set; }

        public abstract CognitionState Update();

        protected IExtendedStateBuilder DisposeCurrent()
        {
            IsDisposed = true;
            return _nextStateBuilder;
        }

        protected IStateBuilder RememberCurrent()
        {
            return _nextStateBuilder;
        }

        public virtual void OnDrawGizmos()
        {
            var guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.normal.textColor = new Color(1.0f, 0.0f, 0.5f, 1.0f);
            Handles.Label(Unit.Position + Vector3.up * 0.75f, GetType().Name, guiStyle);
        }
    }
}