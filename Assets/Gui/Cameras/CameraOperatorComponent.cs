using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraOperatorComponent : MonoBehaviour
    {
        public GameObject FocusObject;
        private void Awake()
        {
        }

        private Vector3 _focusPointInBoardSpace;
        public Vector3 FocusPointInBoardSpace
        {
            get { return _focusPointInBoardSpace; }
        }

        public float MaximalLookDistance = 4;
        public float FocusingTime = 1;
        private Vector3 _velocity;

        private void Update()
        {
            //RaycastHit hit;
            //_guiStore.Raycasts.ScreenPointToEmptyTargetingRay(Input.mousePosition, out hit);
            //var targetPoint = FocusObject.transform.position +
            //                  (hit.point - FocusObject.transform.position).normalized * Mathf.Min(MaximalLookDistance, (hit.point - FocusObject.transform.position).magnitude / 2);
            //FocusPointInBoardSpace = Vector3.SmoothDamp(FocusPointInBoardSpace, targetPoint, ref _velocity, FocusingTime);

            _focusPointInBoardSpace = transform.InverseTransformPoint(FocusObject.transform.position);
            _focusPointInBoardSpace.z = 0;
        }
    }
}