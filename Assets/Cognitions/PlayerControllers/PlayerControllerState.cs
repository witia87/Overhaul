using Assets.Cognitions.PlayerControllers.Controllers;

namespace Assets.Cognitions.PlayerControllers
{
    public abstract class PlayerControllerState : CognitionState<PlayerControllerStateIds>
    {
        protected IMovementController MovementController;
        protected ITargetingController TargetingController;

        protected PlayerControllerState(Cognition<PlayerControllerStateIds> parentCognition,
            ITargetingController targetingController, IMovementController movementController,
            PlayerControllerStateIds id)
            : base(parentCognition, id)
        {
            TargetingController = targetingController;
            MovementController = movementController;
        }
    }
}