using System;
using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Cognitions.Vision
{
    public class Target : ITarget
    {
        private bool _isMemorized;
        private Vector3 _memorizedLastSeenPosition;
        private float _memorizedLastSeenTime;
        private Vector3 _memorizedLastSeenVelocity;
        private Unit _unit;

        public Target(Unit unit)
        {
            _unit = unit;
        }

        public IUnitControl BodyControl { get; private set; }
        public bool IsVisible { get; set; }

        public Vector3 Position
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException(
                        "Target is no longer visible, and must be treated as a memory.");
                }

                return _unit.LogicPosition;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException(
                        "Target is no longer visible, and must be treated as a memory.");
                }

                return _unit.Velocity;
            }
        }

        public Vector3 Center
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException(
                        "Target is no longer visible, and must be treated as a memory.");
                }

                return _unit.Position;
            }
        }

        public float LastSeenTime
        {
            get
            {
                if (_isMemorized)
                {
                    return _memorizedLastSeenTime;
                }

                return Time.time;
            }
        }

        public Vector3 LastSeenPosition
        {
            get
            {
                if (_isMemorized)
                {
                    return _memorizedLastSeenPosition;
                }

                return _unit.LogicPosition;
            }
        }

        public Vector3 LastSeenVelocity
        {
            get
            {
                if (_isMemorized)
                {
                    return _memorizedLastSeenVelocity;
                }

                return _unit.Velocity;
            }
        }

        public void Memorize()
        {
            if (_isMemorized)
            {
                throw new ApplicationException("Target already memorized."); // TODO: Remove later
            }

            _isMemorized = true;
            _memorizedLastSeenPosition = _unit.LogicPosition;
            _memorizedLastSeenVelocity = _unit.Velocity;
            _memorizedLastSeenTime = Time.time;
        }
    }
}