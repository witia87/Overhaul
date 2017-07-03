using System;
using Assets.Cognitions.PathFinders;
using Assets.Map;
using UnityEngine;
using Assets.Modules;
using Assets.Modules.Movement;
using Assets.Modules.Turrets;

namespace Assets.Cognitions
{
    public abstract class Cognition<TStateUids> : MonoBehaviour
    {
        protected ICognitionState<TStateUids> CurrentState;
        [Range(1, 5)] public int MapSamplingSize = 5;

        public IMapStore MapStore;

        public IPathFinder PathFinder { get; private set; }

        public TurretModule TurretModule;

        public MovementModule MovementModule;

        public int Scale
        {
            get { return MapSamplingSize; }
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
            CurrentState = CurrentState.Update();
        }

        private void OnDrawGizmos()
        {
            if (CurrentState != null)
            {
                CurrentState.OnDrawGizmos();
            }
        }
    }
}