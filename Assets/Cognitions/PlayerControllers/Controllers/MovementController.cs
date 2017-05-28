﻿using Assets.Map;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public class MovementController : IMovementController
    {
        public bool IsMovementPresent { get; private set; }
        public Vector3 MovementVector { get; private set; }
        public bool IsJumpPressed { get; private set; }

        Quaternion _rotationQuaternion;

        public void Start()
        {
            _rotationQuaternion = Quaternion.AngleAxis(
                GameMechanics.Stores.CameraStore.CameraEulerAngles.y, Vector3.up);
        }

        public void Update()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            var verticalAxis = Input.GetAxis("Vertical");
            MovementVector = Normalize(new Vector3(horizontalAxis, 0, verticalAxis));
            MovementVector = _rotationQuaternion*MovementVector;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                MovementVector *= 0.66f;
            }
            if (Mathf.Abs(horizontalAxis) > 0.001 || Mathf.Abs(verticalAxis) > 0.001)
            {
                IsMovementPresent = true;
            }
            else
            {
                IsMovementPresent = false;
            }

            IsJumpPressed = Input.GetButtonDown("Jump");
        }

        protected Vector3 Normalize(Vector3 v)
        {
            var magnitude = Mathf.Min(1, v.magnitude);
            return v.normalized*magnitude;
        }
    }
}