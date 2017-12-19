using Assets.Maps;
using Assets.Units;
using Assets.Vision;
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

        public void ManageAimingAtTheTarget(ITarget target)
        {
            _unit.LookAt(target.Center);
            _unit.Gun.AimAt(target.Center);
            var ray = target.Center - _unit.Gun.FirePosition;
            ray.y = 0;
            if (Vector3.Angle(ray, _unit.Gun.Direction) < 10)
            {
                _unit.Gun.Fire();
            }
        }
    }
}