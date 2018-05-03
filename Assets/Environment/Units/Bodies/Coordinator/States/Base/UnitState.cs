using UnityEditor;
using UnityEngine;

namespace Assets.Environment.Units.Bodies.Coordinator.States.Base
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
            var guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.normal.textColor = new Color(1.0f, 0.0f, 0.5f, 1.0f);
            Handles.Label(Torso.transform.position + Vector3.up, GetType().Name, guiStyle);
        }

        protected Vector3 GetLogicVector(Vector3 vector)
        {
            vector.y = 0;
            return vector.normalized;
        }
    }
}