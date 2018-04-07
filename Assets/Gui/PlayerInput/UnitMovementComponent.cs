﻿using Assets.Cognitions.Players;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class UnitMovementComponent : GuiComponent
    {
        [SerializeField] private PlayerCognition _playerCognition;

        private Quaternion _rotationQuaternion;

        public void Start()
        {
            _rotationQuaternion = Quaternion.AngleAxis(
                CameraStore.CameraEulerAngles.y, Vector3.up);
        }

        private void Update()
        {
            if (KeyboardStore.IsMovementPresent)
            {
                _playerCognition.SetMovement(_rotationQuaternion * KeyboardStore.MovementVector);
            }

            if (KeyboardStore.IsCrouchPressed)
            {
                _playerCognition.Crouch();
            }
        }
    }
}