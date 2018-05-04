using System.Collections.Generic;
using Assets.Cognitions.Helpers;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Maps.PathFinders;
using Assets.Cognitions.States;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using Assets.Gui.Cameras;
using Assets.Resources;
using UnityEngine;

namespace Assets.Cognitions
{
    public class Cognition : MonoBehaviour
    {
        private readonly List<CognitionState> _registeredStates = new List<CognitionState>();

        [SerializeField] private int _rememberedStatesCount = 10;

        private Unit _unit;
        protected CognitionState DefaultState;

        protected IMap Map;

        public IMapStore MapStore;
        [Range(0, 5)] public int Scale = 1;

        public IPathFinder PathFinder { get; private set; }

        protected CognitionState CurrentState
        {
            get { return _registeredStates[_registeredStates.Count - 1]; }
        }

        protected virtual void Awake()
        {
            MapStore = FindObjectOfType<MapStore>();
            _unit = GetComponent<Unit>();
        }

        protected virtual void Start()
        {
            Map = MapStore.GetMap(Scale, _unit.Fraction);
            DefaultState = new Watching(new MovementHelper(_unit, Map), new TargetingHelper(_unit, Map),
                _unit, Map, FindObjectOfType<VisionStore>().GetVisionObserver(_unit),
                null);
        }

        protected virtual void Update()
        {
            if (_registeredStates.Count == 0)
            {
                _registeredStates.Add(DefaultState);
            }

            var newCurrentState = CurrentState.Update();
            if (newCurrentState != null)
            {
                if (CurrentState.IsDisposed)
                {
                    _registeredStates.RemoveAt(_registeredStates.Count - 1);
                }

                if (CurrentState == null || CurrentState != newCurrentState)
                {
                    _registeredStates.Add(newCurrentState);
                    if (_registeredStates.Count > _rememberedStatesCount)
                    {
                        _registeredStates.RemoveAt(0);
                    }
                }
            }
            else
            {
                _registeredStates.RemoveAt(_registeredStates.Count - 1);
            }
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying && DebugStore.IsDebugMode
                                      && _registeredStates.Count > 0 && CurrentState != null)
            {
                CurrentState.OnDrawGizmos();
            }
        }
    }
}