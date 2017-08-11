using Assets.Maps;
using Assets.Modules;
using Assets.Modules.Targeting.Vision;
using UnityEngine;

namespace Assets.Cognitions.Helpers
{
    public class TargetingHelper
    {
        private readonly IMap _map;
        private readonly IUnitControl _unit;

        public TargetingHelper(IUnitControl unit, IMap map)
        {
            _unit = unit;
            _map = map;
        }

        /*public virtual ITarget GetHighestPriorityTarget()
        {
            ITarget highestPriorityTargetSoFar = null;
            var minDistance = float.MaxValue;
            if (_unit.Targeting != null)
            {
                var visionSensor = _unit.Targeting.VisionSensor;
                foreach (var testedTarget in visionSensor.VisibleTargets)
                {
                    var currentDistance =
                        (testedTarget.Position - visionSensor.SightPosition).magnitude;
                    if (currentDistance < minDistance)
                    {
                        highestPriorityTargetSoFar = testedTarget;
                        minDistance = currentDistance;
                    }
                }
            }
            return highestPriorityTargetSoFar;
        }*/

        public void ManageAimingAtTheTarget(ITarget target)
        {
            _unit.Targeting.LookAt(target.Center);
            if (_unit.Targeting.IsGunMounted)
            {
                var ray = target.Center - _unit.Targeting.Gun.FirePosition;
                ray.y = 0;
                if (Vector3.Angle(ray, _unit.Targeting.Gun.FireDirection) < 10)
                {
                    _unit.Targeting.Gun.Fire(ray.magnitude, target.Center.y);
                }
                else
                {
                    _unit.Targeting.Gun.StopFiring();
                }
            }
        }
    }
}