﻿using Assets.Cognitions.Maps;
using Assets.Cognitions.Vision;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Helpers
{
    public class TargetingHelper
    {
        private readonly IMap _map;
        private readonly IUnit _unit;

        public TargetingHelper(IUnit unit, IMap map)
        {
            _unit = unit;
            _map = map;
        }

        public void ManageAimingAtTheTarget(ITarget target)
        {
            _unit.Control.LookAt(target.Center);
            _unit.Control.LookAt(target.Center);
            var ray = target.Center - _unit.Gun.Position;
            ray.y = 0;
            if (Vector3.Angle(ray, _unit.Gun.Direction) < 10)
            {
                _unit.Control.Fire();
            }
        }
    }
}