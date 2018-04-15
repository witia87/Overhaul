using System;
using UnityEngine;

namespace Assets.Cognitions.Vision
{
    internal class Target : ITarget
    {
        private bool _isMemorized;
        private Vector3 _memorizedLastSeenPosition;
        private float _memorizedLastSeenTime;
        private Vector3 _memorizedLastSeenVelocity;
        private VisibleUnit _unit;


        public Target(VisibleUnit targetedUnit)
        {
            _unit = targetedUnit;
        }

        public bool IsVisible
        {
            get { return _unit.IsVisible; }
        }

        public Vector3 Position
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException(
                        "Target is no longer visible, and must be treated as a memory.");
                }

                return _unit.Unit.LogicPosition;
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

                return _unit.Unit.Velocity;
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

                return _unit.Unit.Position;
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

                return _unit.Unit.LogicPosition;
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

                return _unit.Unit.Velocity;
            }
        }

        public void Memorize()
        {
            if (_isMemorized)
            {
                throw new ApplicationException("Target already memorized."); // TODO: Remove later
            }

            _isMemorized = true;
            _memorizedLastSeenPosition = _unit.Unit.LogicPosition;
            _memorizedLastSeenVelocity = _unit.Unit.Velocity;
            _memorizedLastSeenTime = Time.time;
        }
    }
}