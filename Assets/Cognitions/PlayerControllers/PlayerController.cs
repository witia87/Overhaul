using Assets.Cognitions.PlayerControllers.Controllers;
using Assets.Cognitions.PlayerControllers.States;
using Assets.Gui.MainCamera;

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
                    var localPosition = ModulesNetwork.gameObject.transform.localPosition;
                    var localRotation = ModulesNetwork.gameObject.transform.localRotation;
                    ModulesNetwork.gameObject.transform.parent = CoreDetector.TargetCore.gameObject.transform;
                    ModulesNetwork.gameObject.transform.localPosition = localPosition;
                    ModulesNetwork.gameObject.transform.localRotation = localRotation;
                    ModulesNetwork = CoreDetector.TargetCore;
                    _connectionCooldownLeft = ConnectionCooldown;
                }
            }
            _connectionCooldownLeft = Mathf.Max(0, _connectionCooldownLeft - Time.deltaTime);
        }*/

        protected override void Start()
        {
            var cameraStore = FindObjectOfType<CameraComponent>() as ICameraStore;
            _targetingController = new MouseTargetingController(cameraStore);
            _movementController = new MouseMovementController(cameraStore);
            _targetingController.Start();
            _movementController.Start();

            CurrentState = new ControllingHumanoid(this, _targetingController, _movementController);
        }

        protected override void Update()
        {
            _targetingController.Update();
            _movementController.Update();
            base.Update();
        }
    }
}