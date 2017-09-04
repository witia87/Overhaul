using System;
using Assets.NewUnits.Helpers;
using UnityEngine;

namespace Assets.NewUnits
{
    public class UnitControlParameters : IUnitControlParameters
    {
        private readonly AngleCalculator _angleCalculator = new AngleCalculator();
        private Vector3 _lookGlobalDirection;
        private Vector3 _moveGlobalDirection;
        public bool IsMoveGlobalDirectionSet { get; set; }

        public Vector3 MoveGlobalDirection
        {
            get
            {
                if (!IsMoveGlobalDirectionSet)
                {
                    throw new ApplicationException("Incorrect UnitControlParaget get.");
                    // TODO: simplify setters after final stabilization is done
                }
                return _moveGlobalDirection;
            }
            set
            {
                _angleCalculator.CheckIfVectorIsLogic(value);
                _moveGlobalDirection = value;
            }
        }

        public bool IsLookGlobalDirectionSet { get; set; }

        public Vector3 LookGlobalDirection
        {
            get
            {
                if (!IsLookGlobalDirectionSet)
                {
                    throw new ApplicationException("Incorrect UnitControlParameter get.");
                    // TODO: simplify setters after final stabilization is done
                }
                return _lookGlobalDirection;
            }
            set
            {
                if (value != value.normalized || Mathf.Abs(value.y) > 0.000001f)
                {
                    throw new ApplicationException("Incorrect UnitControlParameter set.");
                    // TODO: simplify setters after final stabilization is done
                }
                _lookGlobalDirection = value;
            }
        }
    }
}