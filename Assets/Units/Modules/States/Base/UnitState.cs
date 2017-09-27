using Assets.Units.Helpers;
using UnityEngine;

namespace Assets.Units.Modules.States.Base
{
    public abstract class UnitState
    {
        protected AngleCalculator AngleCalculator = new AngleCalculator();

        protected MovementModule Movement;
        protected UnitStatesFactory StatesFactory;
        protected TargetingModule Targeting;

        protected UnitState(MovementModule movement, TargetingModule targeting, UnitStatesFactory statesFactory)
        {
            Movement = movement;
            Targeting = targeting;
            StatesFactory = statesFactory;
        }

        public virtual UnitState VerifyPhysicConditions()
        {
            return this;
        }
        public virtual UnitState Move(Vector3 moveLogicDirection, float speedModifier)
        {
            return this;
        }
        public virtual UnitState StopMoving()
        {
            return this;
        }
        public virtual UnitState Jump(Vector3 globalDirection, float forceModifier)
        {
            return this;
        }
        public virtual UnitState LookTowards(Vector3 globalDirection)
        {
            return this;
        }

        public abstract UnitState FixedUpdate();

        public virtual void OnDrawGizmos()
        {
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