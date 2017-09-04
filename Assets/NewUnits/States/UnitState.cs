using Assets.NewUnits.Helpers;
using UnityEngine;

namespace Assets.NewUnits.States
{
    public abstract class UnitState
    {
        protected AngleCalculator AngleCalculator = new AngleCalculator();

        protected Vector3 Limiter;
        protected MovementModule Movement;
        protected UnitControlParameters Parameters;
        protected UnitStates States;
        protected TargetingModule Targeting;

        protected UnitState(MovementModule movement, TargetingModule targeting, UnitControlParameters parameters,
            UnitStates states)
        {
            Movement = movement;
            Targeting = targeting;
            Parameters = parameters;
            States = states;
        }

        public abstract void FixedUpdate();

        protected void ManageReachingMovementTurnDirection(Vector3 movementTurnDirection)
        {
            var angle = AngleCalculator.CalculateLogicAngle(Movement.ModuleLogicDirection,
                movementTurnDirection);
            if (Mathf.Abs(angle) < 60)
            {
                Movement.AddRotation(Vector3.Cross(Movement.ModuleLogicDirection, movementTurnDirection).y);
            }
            else if (AngleCalculator.CalculateLogicAngle(Parameters.LookGlobalDirection,
                         Limiter) >= 0)
            {
                Movement.AddRotation(1);
            }
            else
            {
                Movement.AddRotation(-1);
            }
        }

        protected void ManageReachingTargetingTurnDirection()
        {
            var angle = AngleCalculator.CalculateLogicAngle(Movement.ModuleLogicDirection,
                Parameters.LookGlobalDirection);
            if (Mathf.Abs(angle) < 60)
            {
                Targeting.AddRotation(Vector3.Cross(Targeting.ModuleLogicDirection, Parameters.LookGlobalDirection).y);
            }
            else if (AngleCalculator.CalculateLogicAngle(Parameters.LookGlobalDirection,
                         Limiter) >= 0)
            {
                var direction = AngleCalculator.RotateLogicVector(Movement.ModuleLogicDirection, 60);
                Targeting.AddRotation(Vector3.Cross(Targeting.ModuleLogicDirection, direction).y);
            }
            else
            {
                var direction = AngleCalculator.RotateLogicVector(Movement.ModuleLogicDirection, -60);
                Targeting.AddRotation(Vector3.Cross(Targeting.ModuleLogicDirection, direction).y);
            }
        }

        /// <summary>
        ///     Limiter is a vector shows wich parts of the circle should result on turning left, or right.
        ///     It's calcuated based on Momement.ModuleLogicDirection and its angular velocity
        ///     (if the rigidbody is not turning, then the limiter is 180dg from the Movement.ModuleLogicDirection;
        ///     if the movement rigidbody is currently turning right, then the further the limiter is turned to the right)
        /// </summary>
        protected void CalculateLimiter()
        {
            var angleModifier = Movement.transform.InverseTransformDirection(Movement.Rigidbody.angularVelocity).y *
                                10;
            angleModifier = Mathf.Sign(angleModifier) * Mathf.Min(120, Mathf.Abs(angleModifier));
            Limiter = AngleCalculator.RotateLogicVector(-Movement.ModuleLogicDirection, angleModifier);
        }

        public void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(Movement.transform.position, Movement.transform.position + Limiter);
            }
        }

        public virtual void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            var guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.fontSize = h * 2 / 100;
            guiStyle.normal.textColor = new Color(1.0f, 0.0f, 0.5f, 1.0f);
            var rect = new Rect(w - 200, 0, w, h * 2 / 100);
            GUI.Label(rect, GetType().Name, guiStyle);
        }
    }
}