using System;
using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Vision
{
    public class Target : ITarget
    {
        private bool _isMemorized;
        private Vector3 _memorizedLastSeenPosition;
        private float _memorizedLastSeenTime;
        private Vector3 _memorizedLastSeenVelocity;

        public Target(HeadModule headModule)
        {
            HeadModule = headModule;
        }

        public IUnitControl HeadModule { get; private set; }
        public bool IsVisible { get; set; }

        public Vector3 Position
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException("Target is no longer visible, and must be treated as a memory.");
                }
                return HeadModule.LogicPosition;
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
                return HeadModule.Velocity;
            }
        }

        public Vector3 Center
        {
            get
            {
                if (!IsVisible)
                {
                    throw new ApplicationException("Target is no longer visible, and must be treated as a memory.");
                }
                return HeadModule.Center;
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
                return HeadModule.LogicPosition;
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
                return HeadModule.Velocity;
            }
        }

        public void Memorize()
        {
            if (_isMemorized)
            {
                throw new ApplicationException("Target already memorized."); // TODO: Remove later
            }
            _isMemorized = true;
            _memorizedLastSeenPosition = HeadModule.LogicPosition;
            _memorizedLastSeenVelocity = HeadModule.Velocity;
            _memorizedLastSeenTime = Time.time;
        }
    }
}