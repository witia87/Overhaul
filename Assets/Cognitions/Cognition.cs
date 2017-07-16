using System.Collections.Generic;
using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class Cognition<TStateUids> : MonoBehaviour
    {
        private readonly List<ICognitionState<TStateUids>> _registeredStates = new List<ICognitionState<TStateUids>>();

        [SerializeField] private readonly int _rememberedStatesCount = 10;

        public Unit ConnectedUnit;
        protected CognitionState<TStateUids> DefaultState;
        [Range(1, 5)] public int MapSamplingSize = 5;

        public IMapStore MapStore;

        public IPathFinder PathFinder { get; private set; }

        public int Scale
        {
            get { return MapSamplingSize; }
        }

        protected ICognitionState<TStateUids> CurrentState
        {
            get { return _registeredStates[_registeredStates.Count - 1]; }
        }

        protected virtual void Awake()
        {
            MapStore = FindObjectOfType<MapStore>();
        }

        protected virtual void Start()
        {
            PathFinder = new SimplePathFinder(MapStore, MapSamplingSize);
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
            if (_registeredStates.Count > 0 && CurrentState != null)
            {
                CurrentState.OnDrawGizmos();
            }
        }
    }
}