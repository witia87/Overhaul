using System;
using Assets.Cognitions.PathFinders;
using Assets.Cores;
using Assets.Map;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class Cognition<TStateUids> : MonoBehaviour, ICognition
    {
        public Core Core;
        protected ICognitionState<TStateUids> CurrentState;
        [Range(1, 5)] public int MapSamplingSize = 5;

        public IMapStore MapStore;

        public MountedModules MountedModules
        {
            get { return Core.MountedModules; }
        }

        public IPathFinder PathFinder { get; private set; }


        public int Scale
        {
            get { return MapSamplingSize; }
        }

        public bool IsConnected
        {
            get { return MountedModules != null; }
        }

        protected virtual void Awake()
        {
            MapStore = FindObjectOfType<MapStore>() as IMapStore;
        }

        protected virtual void Start()
        {
            PathFinder = new SimplePathFinder(MapStore, MapSamplingSize);
        }

        protected virtual void Update()
        {
            if (IsConnected)
            {
                CurrentState = CurrentState.Update();
            }
        }

        protected virtual void ManageMovement(GameObject targetGameObject)
        {
            var path = PathFinder.FindPath(gameObject.transform.position, targetGameObject.transform.position);
            var direction = targetGameObject.transform.position - gameObject.transform.position;
            var distance = direction.magnitude;
            MountedModules.VehicleMovementControl.MoveForward(1);
            if (distance > 1)
            {
                var angle = Vector3.Angle(direction, MountedModules.VehicleMovementControl.UnitDirection);
                if (angle < 0)
                {
                    throw new ApplicationException("Angle is less than 0.");
                }
                if (angle > 5)
                {
                    MountedModules.VehicleMovementControl.TurnFrontTowards(direction);
                }
            }
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