using Assets.Cognitions.PathFinders;
using Assets.Map;
using Assets.Modules;
using Assets.Modules.Movement;
using Assets.Modules.Targeting;
using UnityEngine;

namespace Assets.Cognitions
{
    public abstract class CognitionState<TStateIds> : ICognitionState<TStateIds>
    {
        protected readonly IMapStore MapStore;
        protected readonly Cognition<TStateIds> ParentCognition;

        protected CognitionState(Cognition<TStateIds> parentCognition, TStateIds id)
        {
            Id = id;
            ParentCognition = parentCognition;
            MapStore = parentCognition.MapStore;
        }

        protected IPathFinder PathFinder
        {
            get { return ParentCognition.PathFinder; }
        }

        protected IUnitControl Unit
        {
            get { return ParentCognition.ConnectedUnit; }
        }
        
        protected int Scale
        {
            get { return ParentCognition.Scale; }
        }

        public TStateIds Id { get; private set; }

        public abstract ICognitionState<TStateIds> Update();

        public virtual void OnDrawGizmos()
        {
        }

        protected virtual Module GetHighestPriorityTarget()
        {
            Module highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            if (Unit.Targeting != null)
            {
                var visionSensor = Unit.Targeting.VisionSensor;
                foreach (var testedModule in visionSensor.VisibleModules)
                {
                    var currentDistance =
                        (testedModule.transform.position - visionSensor.SightPosition).magnitude;
                    if (currentDistance < minDistance)
                    {
                        highestPriorityTargetSoFar = testedModule;
                        minDistance = currentDistance;
                    }
                }
            }
            return highestPriorityTargetSoFar;
        }


        protected Vector3 ClampVector(Vector3 v)
        {
            return new Vector3(
                Mathf.Min(MapStore.MapWidth, Mathf.Max(0, v.x)),
                0,
                Mathf.Min(MapStore.MapLength, Mathf.Max(0, v.z)));
        }
    }
}