using Assets.Cognitions.Helpers;
using Assets.Cognitions.Players.Controllers;
using Assets.Maps;
using Assets.Units;

namespace Assets.Cognitions.Players
{
    public abstract class PlayerState : CognitionState<PlayerStateIds>
    {
        protected IActionsController ActionsController;
        protected IMovementController MovementController;
        protected ITargetingController TargetingController;

        protected PlayerState(PlayerStateIds id, IUnitControl unit, IMap map,
            ITargetingController targetingController,
            IMovementController movementController,
            IActionsController actionsController) :
            base(id, new MovementHelper(unit, map), new TargetingHelper(unit, map), unit, map)
        {
            TargetingController = targetingController;
            MovementController = movementController;
            ActionsController = actionsController;
        }
    }
}