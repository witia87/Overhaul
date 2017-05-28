using System;
using Assets.Cognitions.PlayerControllers.Controllers;
using Assets.Cognitions.PlayerControllers.States;
using Assets.Cores;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers
{
    public class PlayerController : Cognition<PlayerControllerStateIds>
    {
        private MouseMovementController _movementController;

        private MouseTargetingController _targetingController;
        /* private const float ConnectionCooldown = 1;
        private float _connectionCooldownLeft;
        
        protected void ManageConnection()
        {
            if (Input.GetButton("Connect"))
            {
                if (CoreDetector.TargetCore != null && _connectionCooldownLeft <= 0)
                {
                    var localPosition = Core.gameObject.transform.localPosition;
                    var localRotation = Core.gameObject.transform.localRotation;
                    Core.gameObject.transform.parent = CoreDetector.TargetCore.gameObject.transform;
                    Core.gameObject.transform.localPosition = localPosition;
                    Core.gameObject.transform.localRotation = localRotation;
                    Core = CoreDetector.TargetCore;
                    _connectionCooldownLeft = ConnectionCooldown;
                }
            }
            _connectionCooldownLeft = Mathf.Max(0, _connectionCooldownLeft - Time.deltaTime);
        }*/
        public UnityEngine.Camera Camera;

        protected override void Start()
        {
            _targetingController = new MouseTargetingController(Camera);
            _movementController = new MouseMovementController();
            _targetingController.Start();
            _movementController.Start();

            switch (Core.Type)
            {
                case CoreTypeIds.Vehicle:
                    CurrentState = new ControllingVehicle(this, _targetingController, _movementController);
                    break;
                case CoreTypeIds.Humanoid:
                    CurrentState = new ControllingHumanoid(this, _targetingController, _movementController);
                    break;
                default:
                    throw new ApplicationException("Core Type not recognized.");
            }
        }

        protected override void Update()
        {
            _targetingController.Update();
            _movementController.Update();
            base.Update();
        }
    }
}