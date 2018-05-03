using Assets.Environment;
using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class PadTargetingController
    {
        private readonly CameraStore _cameraStore;

        private float _horizontalAxis;
        private float _verticalAxis;

        public PadTargetingController(CameraStore cameraStore)
        {
            _cameraStore = cameraStore;
        }

        public Vector3 TargetedPosition { get; private set; }

        public bool IsTargetPresent
        {
            get { return TargetedModule != null; }
        }

        public Module TargetedModule { get; private set; }
        public bool IsFirePressed { get; private set; }

        public void Update()
        {
            //var cameraFocusPoint = _cameraStore.FocusPoint;

            if (Mathf.Abs(Input.GetAxis("HorizontalLook")) > 0.25f ||
                Mathf.Abs(Input.GetAxis("VerticalLook")) > 0.25f)
            {
                _horizontalAxis = Input.GetAxis("HorizontalLook");
                _verticalAxis = Input.GetAxis("VerticalLook");
            }

            var targetingVector = new Vector3(_horizontalAxis, 0, _verticalAxis);
            targetingVector = Quaternion.AngleAxis(45, Vector3.up) * targetingVector;
            //TargetedPosition = cameraFocusPoint + targetingVector;

            IsFirePressed = Input.GetButton("Fire Gun");
            //IsFirePressed = Input.GetAxis("Fire Gun") > 0.5f;
        }

        public void OnDrawGizmos()
        {
            Debug.DrawLine(TargetedPosition,
                TargetedPosition + Vector3.up,
                Color.red);
        }
    }
}