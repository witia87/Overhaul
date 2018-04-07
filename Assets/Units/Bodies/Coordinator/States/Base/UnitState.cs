using UnityEngine;

namespace Assets.Units.Modules.Coordinator.States.Base
{
    public abstract class UnitState
    {
        protected LegsModule Legs;
        protected IUnitControlParameters Parameters;
        protected TorsoModule Torso;

        protected UnitState(TorsoModule torso, LegsModule legs, IUnitControlParameters parameters)
        {
            Legs = legs;
            Torso = torso;
            Parameters = parameters;
        }

        public abstract UnitState FixedUpdate();

        public virtual void OnDrawGizmos()
        {
        }

        protected Vector3 GetLogicVector(Vector3 vector)
        {
            vector.y = 0;
            return vector.normalized;
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