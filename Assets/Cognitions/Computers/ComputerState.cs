using Assets.Cognitions.Helpers;
using Assets.Maps;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cognitions.Computers
{
    public abstract class ComputerState : CognitionState<ComputerStateIds>
    {
        protected ComputerState(ComputerStateIds id, MovementHelper movementHelper, TargetingHelper targetingHelper, IUnitControl unit, IMap map) : 
            base(id, movementHelper, targetingHelper, unit, map)
        {
            StatesFactory = new ComputerStatesFactory(map, unit, movementHelper, targetingHelper);
        }

        protected readonly ComputerStatesFactory StatesFactory;

        public override void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            var guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.UpperLeft;
            guiStyle.fontSize = h * 2 / 100;
            guiStyle.normal.textColor = new Color(1.0f, 0.0f, 0.5f, 1.0f);
            var rect = new Rect(w - 100, 0, w, h * 2 / 100);
            GUI.Label(rect, Id.ToString(), guiStyle);
        }
    }
}