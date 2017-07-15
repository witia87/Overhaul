using Assets.Cognitions.Player.Controllers;
using Assets.Cognitions.Player.States;
using Assets.Gui.MainCamera;

namespace Assets.Cognitions.Player
{
    public class Player : Cognition<PlayerStateIds>
    {
        private KeyboardActionsController _actionsController;
        private KeyboardMovementController _movementController;

        private MouseTargetingController _targetingController;

        protected override void Start()
        {
            var cameraStore = FindObjectOfType<CameraComponent>() as ICameraStore;
            _targetingController = new MouseTargetingController(cameraStore);
            _movementController = new KeyboardMovementController(cameraStore);
            _actionsController = new KeyboardActionsController();
            _targetingController.Start();
            _movementController.Start();

            CurrentState = new ControllingHumanoid(this, _targetingController, _movementController, _actionsController);
        }

        protected override void Update()
        {
            _targetingController.Update();
            _movementController.Update();
            _actionsController.Update();
            base.Update();
        }
    }
}