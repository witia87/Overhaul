using System;
using UnityEngine;

namespace Assets.Modules.Units.Bodies.Coordinator.States.Base
{
    public abstract class GroundedState : UnitState
    {
        protected GroundedState(LegsModule legs, TorsoModule torso, IUnitControlParameters parameters) :
            base(torso, legs, parameters)
        {
        }

        public Vector3 GetLogicDirection(Vector3 moduleForward)
        {
            if (Mathf.Abs(moduleForward.y) > 0.999f) // TODO: Verify whether this condition might be removed.
            {
                throw new ApplicationException(
                    "Cannot calculate ModuleLogicDirection when module lays horizontaly.");
            }

            return GetLogicVector(moduleForward);
        }
    }
}