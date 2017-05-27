using Assets.Cognitions.PathFinders;
using Assets.Cores;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds> : ICognitionState<TStateIds>
    {
        protected readonly Cognition<TStateIds> ParrentCognition;

        protected CognitionState(Cognition<TStateIds> parrentCognition, TStateIds id)
        {
            Id = id;
            ParrentCognition = parrentCognition;
        }

        protected bool IsConnected
        {
            get { return ParrentCognition.IsConnected; }
        }

        protected MountedModules MountedModules
        {
            get { return ParrentCognition.MountedModules; }
        }

        protected IPathFinder PathFinder
        {
            get { return ParrentCognition.PathFinder; }
        }

        protected Core Core
        {
            get { return ParrentCognition.Core; }
            set { ParrentCognition.Core = value; }
        }

        protected int Scale
        {
            get { return ParrentCognition.Scale; }
        }

        public TStateIds Id { get; private set; }

        public abstract ICognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        protected virtual Core GetHighestPriorityTarget()
        {
            Core highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            foreach (var visionSensor in MountedModules.TurretControls)
            {
                foreach (var testedGameObject in visionSensor.VisibleCores)
                {
                    var currentDistance =
                        (testedGameObject.gameObject.transform.position - Core.gameObject.transform.position).magnitude;
                    if (currentDistance < minDistance)
                    {
                        highestPriorityTargetSoFar = testedGameObject;
                        minDistance = currentDistance;
                    }
                }
            }
            return highestPriorityTargetSoFar;
        }


        protected Vector3 ClampVector(Vector3 v)
        {
            return new Vector3(
                Mathf.Min(GameMechanics.Stores.MapStore.MapWidth, Mathf.Max(0, v.x)),
                0,
                Mathf.Min(GameMechanics.Stores.MapStore.MapLength, Mathf.Max(0, v.z)));
        }
    }
}