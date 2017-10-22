using System.Collections.Generic;
using Assets.Maps;
using Assets.Maps.PathFinders;
using Assets.Units;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class Cognition<TStateUids> : MonoBehaviour
    {
        private readonly List<CognitionState<TStateUids>> _registeredStates = new List<CognitionState<TStateUids>>();

        [SerializeField] private int _rememberedStatesCount = 10;
        protected CognitionState<TStateUids> DefaultState;
        public FractionId Fraction = FractionId.Enemy;

        protected IMap Map;

        public IMapStore MapStore;
        [Range(0, 5)] public int Scale = 1;

        public Unit Unit;

        public IPathFinder PathFinder { get; private set; }

        protected CognitionState<TStateUids> CurrentState
        {
            get { return _registeredStates[_registeredStates.Count - 1]; }
        }

        protected virtual void Awake()
        {
            MapStore = FindObjectOfType<MapStore>();
        }

        protected virtual void Start()
        {
            Map = MapStore.GetMap(Scale, Fraction);
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

        private void OnGUI()
        {
            if (_registeredStates.Count > 0 && CurrentState != null)
            {
                CurrentState.OnGUI();
            }
        }
    }
}