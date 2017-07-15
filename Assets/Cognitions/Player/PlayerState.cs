using Assets.Cognitions.Player.Controllers;

namespace Assets.Cognitions.Player
{
    public abstract class PlayerState : CognitionState<PlayerStateIds>
    {
        protected IActionsController ActionsController;
        protected IMovementController MovementController;
        protected ITargetingController TargetingController;

        protected PlayerState(Cognition<PlayerStateIds> parentCognition,
            ITargetingController targetingController,
            IMovementController movementController,
            IActionsController actionsController,
            PlayerStateIds id)
            : base(parentCognition, id)
        {
            TargetingController = targetingController;
            MovementController = movementController;
            ActionsController = actionsController;
        }
    }
}