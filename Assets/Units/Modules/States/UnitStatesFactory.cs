using UnityEngine;

namespace Assets.Units.Modules.States
{
    public class UnitStatesFactory
    {
        private readonly MovementModule _movementModule;
        private readonly TargetingModule _targetingModule;


        public UnitStatesFactory(MovementModule movementModule, TargetingModule targetingModule)
        {
            _movementModule = movementModule;
            _targetingModule = targetingModule;
        }

        public Standing CreateStanding(Vector3 initialLookGlobalDirection)
        {
            return new Standing(_movementModule, _targetingModule, this, initialLookGlobalDirection);
        }

        public MovingForward CreateMovingForward(Vector3 initialLookGlobalDirection, Vector3 initialMoveLogicDirection, float speedModifier)
        {
            return new MovingForward(_movementModule, _targetingModule, this, initialLookGlobalDirection, initialMoveLogicDirection, speedModifier);
        }

        public MovingBackward CreateMovingBackward(Vector3 initialLookGlobalDirection, Vector3 initialMoveLogicDirection, float speedModifier)
        {
            return new MovingBackward(_movementModule, _targetingModule, this, initialLookGlobalDirection, initialMoveLogicDirection, speedModifier);
        }

        public Jumping CreateJumping(Vector3 initialLookGlobalDirection, Vector3 initialJumpLogicDirection, float speedModifier)
        {
            return new Jumping(_movementModule, _targetingModule, this, initialLookGlobalDirection, initialJumpLogicDirection, speedModifier);
        }

        public Gliding CreateGliding(Vector3 initialLookGlobalDirection, Vector3 initialGlideLogicDirection)
        {
            return new Gliding(_movementModule, _targetingModule, this, initialLookGlobalDirection, initialGlideLogicDirection);
        }
    }
}