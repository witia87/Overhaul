using Assets.Environment.Units.Bodies.Coordinator.States;
using Assets.Environment.Units.Bodies.Coordinator.States.Base;

namespace Assets.Environment.Units.Bodies.Coordinator
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
        private Unit _unit;

        public BodyCoordinator(Unit unit, TorsoModule torsoModule, LegsModule legsModule,
            IUnitControlParameters parameters)
        {
            _unit = unit;
            _legs = legsModule;
            _torso = torsoModule;
            _parameters = parameters;
            _standing = new Standing(torsoModule, legsModule, parameters);
            _moving = new Moving(torsoModule, legsModule, parameters);
            _gliding = new Gliding(torsoModule, legsModule, parameters);
            _standingUp = new StandingUp(torsoModule, legsModule, parameters);
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
            if (_unit.IsStunned)
            {
                return;
            }

            _currentState = GetState();
            _currentState.FixedUpdate();

            if (_parameters.IsSetToFire && _torso.Gun)
            {
                _torso.Gun.Fire();
            }
        }

        public void OnDrawGizmos()
        {
            _currentState.OnDrawGizmos();
        }
    }
}