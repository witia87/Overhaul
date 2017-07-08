using Assets.Cognitions.PlayerControllers.Controllers;

namespace Assets.Cognitions.PlayerControllers
{
    public abstract class PlayerControllerState : CognitionState<PlayerControllerStateIds>
    {
        protected IMovementController MovementController;
        protected ITargetingController TargetingController;
        protected IActionsController ActionsController;

        protected PlayerControllerState(Cognition<PlayerControllerStateIds> parentCognition,
            ITargetingController targetingController, 
            IMovementController movementController, 
            IActionsController actionsController,
            PlayerControllerStateIds id)
            : base(parentCognition, id)
        {
            TargetingController = targetingController;
            MovementController = movementController;
            ActionsController = actionsController;
        }
    }
}