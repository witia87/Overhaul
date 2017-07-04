using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Modules.Movement;
using Assets.Modules.Turrets;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class Cognition<TStateUids> : MonoBehaviour
    {
        protected ICognitionState<TStateUids> CurrentState;
        [Range(1, 5)] public int MapSamplingSize = 5;

        public IMapStore MapStore;

        public MovementModule MovementModule;

        public TurretModule TurretModule;

        public IPathFinder PathFinder { get; private set; }

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