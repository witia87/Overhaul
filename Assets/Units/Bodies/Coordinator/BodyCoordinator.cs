using Assets.Units.Modules;
using Assets.Units.Modules.Coordinator.States;
using Assets.Units.Modules.Coordinator.States.Base;

namespace Assets.Units.Bodies.Coordinator
{
    public class BodyCoordinator
    {
        private UnitState _currentState;
        private UnitState _gliding;
        private LegsModule _legs;
        private UnitState _moving;
        private IUnitControlParameters _parameters;
        private UnitState _standing;
        private UnitState _standingUp;
        private TorsoModule _torso;

        public BodyCoordinator(TorsoModule torsoModule, LegsModule legsModule,
            IUnitControlParameters parameters)
        {
            _torso = torsoModule;
            _legs = legsModule;
            _parameters = parameters;
            _standing = new Standing(legsModule, torsoModule, parameters);
            _moving = new Moving(legsModule, torsoModule, parameters);
            _gliding = new Gliding(legsModule, torsoModule, parameters);
            _standingUp = new StandingUp(legsModule, torsoModule, parameters);
        }

        public UnitState GetState()
        {
            if (_legs.IsStanding)
            {
                if (_parameters.IsSetToMove)
                {
                    return _moving;
                }

                return _standing;
            }

            if (_legs.IsGrounded || _torso.IsGrounded)
            {
                return _standingUp;
            }

            return _gliding;
        }

        public void FixedUpdate()
        {
            _currentState = GetState();
            _currentState.FixedUpdate();
        }

        public void OnGUI()
        {
            _currentState.OnGUI();
        }
    }
}