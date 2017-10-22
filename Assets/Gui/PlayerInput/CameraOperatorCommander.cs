using Assets.Gui.Payloads;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class CameraOperatorCommander : GuiCommander
    {
        private Vector2 _focusPointInBoardSpace;

        private Vector2 _velocity;
        public float FocusingTime = 1;
        public GameObject FocusObject;

        public float MaximalLookDistance = 4;

        public Vector3 FocusPointInBoardSpace
        {
            get { return _focusPointInBoardSpace; }
        }
        
        private void Update()
        {
            var focusObjetcPositionInBoardSpace =
                CameraStore.TransformWorldPositionToCameraPlane(FocusObject.transform.position);
            var targetPoint = focusObjetcPositionInBoardSpace +
                              (MouseStore.MousePositionInBoardSpace - focusObjetcPositionInBoardSpace).normalized
                              * Mathf.Min(MaximalLookDistance,
                                  (MouseStore.MousePositionInBoardSpace - focusObjetcPositionInBoardSpace).magnitude /
                                  2);

            _focusPointInBoardSpace = Vector2.SmoothDamp(_focusPointInBoardSpace, targetPoint, ref _velocity,
                FocusingTime, 10000, Time.deltaTime);

            Dispatcher.Dispatch(GuiCommandIds.ChangeFocusPointInBoardSpace,
                new ChangeFocusPointInBoardSpacePayload {Position = _focusPointInBoardSpace });
        }
    }
}