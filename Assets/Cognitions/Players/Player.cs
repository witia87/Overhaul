using Assets.Cognitions.Players.Controllers;
using Assets.Cognitions.Players.States;
using Assets.Gui;
using Assets.Gui.Board;
using Assets.Gui.Cameras;

namespace Assets.Cognitions.Players
{
    public class Player : Cognition<PlayerStateIds>
    {
        private KeyboardActionsController _actionsController;
        private KeyboardMovementController _movementController;

        private MouseTargetingController _targetingController;

        protected override void Start()
        {
            var cameraStore = FindObjectOfType<CameraStore>() as CameraStore;
            var guiStore = FindObjectOfType<BoardStore>();
            _targetingController = new MouseTargetingController(guiStore);
            _movementController = new KeyboardMovementController(cameraStore);
            _actionsController = new KeyboardActionsController();
            _targetingController.Start();
            _movementController.Start();

            DefaultState = new ControllingHumanoid(Unit, Map, _targetingController, _movementController,
                _actionsController);
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