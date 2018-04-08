using System.Collections.Generic;
using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Cognitions.Vision
{
    public class VisionObserver : IVisionObserver
    {
        private Unit _closesVisibleOpposingUnitControl;


        private readonly Vector3[] _raysToMeasureDistances =
        {
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, 0),
            new Vector3(-1, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, -1)
        };

        private Unit _unit;
        private VisionStore _visionStore;

        public VisionObserver(Unit unit, VisionStore visionStore)
        {
            _visionStore = visionStore;
            _unit = unit;
        }

        public List<Unit> VisibleOpposingUnits { get; private set; }

        public bool GetClosestTarget(out ITarget target)
        {
            if (_closesVisibleOpposingUnitControl != null)
            {
                target = new Target(_closesVisibleOpposingUnitControl);
                return true;
            }

            target = null;
            return false;
        }

        public void UpdateVisibleUnits(List<Unit> units)
        {
            VisibleOpposingUnits = units;
            if (VisibleOpposingUnits.Count > 0)
            {
                _closesVisibleOpposingUnitControl = VisibleOpposingUnits[0];
                for (var i = 1; i < VisibleOpposingUnits.Count; i++)
                {
                    if ((_unit.Position - VisibleOpposingUnits[i].Position).magnitude <
                        (_unit.Position - _closesVisibleOpposingUnitControl.Position).magnitude)
                    {
                        _closesVisibleOpposingUnitControl = VisibleOpposingUnits[i];
                    }
                }
            }
            else
            {
                _closesVisibleOpposingUnitControl = null;
            }
        }

        public List<Vector3> GetThreeClosestDirections()
        {
            var distances = new List<float>();
            var directions = new List<Vector3>(_raysToMeasureDistances);
            for (var j = 0; j < 8; j++)
            {
                RaycastHit hit;
                if (Physics.Raycast(_unit.transform.position, _raysToMeasureDistances[j], out hit,
                    _visionStore.WalLayerMask))
                {
                    distances.Add(hit.distance);
                }
            }

            var i = Mathf.FloorToInt(Random.value * 8);
            while (directions.Count > 3)
            {
                if (distances[i] < distances[(i + 1) % directions.Count])
                {
                    distances.RemoveAt(i);
                    directions.RemoveAt(i);
                }

                i = (i + 1) % directions.Count;
            }

            return directions;
        }
    }
}