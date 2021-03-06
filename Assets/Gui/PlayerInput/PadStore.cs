﻿using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class PadMovementController
    {
        private readonly CameraStore _cameraStore;
        private Quaternion _rotationQuaternion;

        public PadMovementController(CameraStore cameraStore)
        {
            _cameraStore = cameraStore;
        }

        public bool IsMovementPresent { get; private set; }
        public Vector3 MovementVector { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsCrouchPressed { get; private set; }

        public void Start()
        {
            _rotationQuaternion = Quaternion.AngleAxis(
                _cameraStore.CameraEulerAngles.y, Vector3.up);
        }

        public void Update()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            var verticalAxis = Input.GetAxis("Vertical");
            MovementVector = Normalize(new Vector3(horizontalAxis, 0, verticalAxis));
            MovementVector = _rotationQuaternion * MovementVector;
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

            IsCrouchPressed = Input.GetButtonDown("Crouch");
        }

        protected Vector3 Normalize(Vector3 v)
        {
            var magnitude = Mathf.Min(1, v.magnitude);
            return v.normalized * magnitude;
        }
    }
}