using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States
{
    public class UnitStatesFactory
    {
        private readonly LegsModule _legsModule;
        private readonly TorsoModule _torsoModule;


        public UnitStatesFactory(LegsModule legsModule, TorsoModule torsoModule)
        {
            _legsModule = legsModule;
            _torsoModule = torsoModule;
        }

        public Standing CreateStanding(Vector3 initialLookGlobalDirection)
        {
            return new Standing(_legsModule, _torsoModule, this, initialLookGlobalDirection);
        }

        public Moving CreateMovingForward(Vector3 initialLookGlobalDirection, Vector3 initialMoveLogicDirection,
            float speedModifier)
        {
            return new Moving(_legsModule, _torsoModule, this, initialLookGlobalDirection,
                initialMoveLogicDirection, speedModifier);
        }

        public Gliding CreateGliding(Vector3 initialLookGlobalDirection)
        {
            return new Gliding(_legsModule, _torsoModule, this, initialLookGlobalDirection);
        }

        public LayingBack CreateLayingBack(Vector3 initialLookGlobalDirection)
        {
            return new LayingBack(_legsModule, _torsoModule, this, initialLookGlobalDirection);
        }

        public Crawling CreateCrawling(Vector3 initialLookGlobalDirection)
        {
            return new Crawling(_legsModule, _torsoModule, this, initialLookGlobalDirection);
        }

        public StandingUp CreateStandingUp(Vector3 initialLookGlobalDirection)
        {
            return new StandingUp(_legsModule, _torsoModule, this, initialLookGlobalDirection);
        }
    }
}