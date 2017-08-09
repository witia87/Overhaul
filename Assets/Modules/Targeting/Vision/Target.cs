using System;
using UnityEngine;

namespace Assets.Modules.Targeting.Vision
{
    public class Target: ITarget
    {
        public bool IsVisible { get; set; }

        public Vector3 Position
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException("Target is no longer visible, and must be treated as a memory.");
                }
                return Unit.Position;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException("Target is no longer visible, and must be treated as a memory.");
                }
                return Unit.Rigidbody.velocity;
            }
        }

        public IUnitControl Unit { get; private set; }

        public Vector3 Center
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException("Target is no longer visible, and must be treated as a memory.");
                }
                return Unit.Targeting.Center;
            }
        }

        public Target(Unit unit)
        {
            Unit = unit;
        }

        private bool _isMemorized;
        private Vector3 _memorizedLastSeenPosition;
        private Vector3 _memorizedLastSeenVelocity;
        private float _memorizedLastSeenTime;
        public void Memorize()
        {
            if (_isMemorized)
            {
                throw new ApplicationException("Target already memorized."); // TODO: Remove later
            }
            _isMemorized = true;
            _memorizedLastSeenPosition = Unit.Position;
            _memorizedLastSeenVelocity = Unit.Rigidbody.velocity;
            _memorizedLastSeenTime = Time.time;

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
                return Unit.Position;
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
                return Unit.Rigidbody.velocity;
            }
        }

    }
}