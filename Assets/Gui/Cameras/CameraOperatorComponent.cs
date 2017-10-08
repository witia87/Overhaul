using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class CameraOperatorComponent : MonoBehaviour
    {
        private GuiStore _guiStore;

        public GameObject FocusObject;
        private void Awake()
        {
            _guiStore = FindObjectOfType<GuiStore>();
        }
        
        public Vector3 FocusPoint { get; private set; }
        public float MaximalLookDistance = 4;
        public float FocusingTime = 1;
        private Vector3 _velocity;
        private void Update()
        {
            RaycastHit hit;
            _guiStore.Raycasts.ScreenPointToEmptyTargetingRay(Input.mousePosition, out hit);
            var targetPoint = FocusObject.transform.position +
                              (hit.point - FocusObject.transform.position).normalized * Mathf.Min(MaximalLookDistance, (hit.point - FocusObject.transform.position).magnitude / 2);
            FocusPoint = Vector3.SmoothDamp(FocusPoint, targetPoint, ref _velocity, FocusingTime);
        }
    }
}