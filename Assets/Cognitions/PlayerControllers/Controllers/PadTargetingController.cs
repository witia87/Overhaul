using Assets.MainCamera;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public class PadTargetingController : ITargetingController
    {
        private readonly ICameraStore _cameraStore;
        private readonly int _floorLayerMask = Layers.Floor;

        private readonly int _targetLayerMask = Layers.Map | Layers.MapTransparent
                                                | Layers.Structure | Layers.StructureTransparent
                                                | Layers.Environment | Layers.EnvironmentTransparent
                                                | Layers.Organism | Layers.OrganismTransparent;

        private float _horizontalAxis;
        private float _verticalAxis;

        public PadTargetingController(ICameraStore cameraStore)
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
            var cameraFocusPoint = _cameraStore.FocusPoint;

            if (Mathf.Abs(Input.GetAxis("HorizontalLook")) > 0.25f || Mathf.Abs(Input.GetAxis("VerticalLook")) > 0.25f)
            {
                _horizontalAxis = Input.GetAxis("HorizontalLook");
                _verticalAxis = Input.GetAxis("VerticalLook");
            }
            var targetingVector = new Vector3(_horizontalAxis, 0, _verticalAxis);
            targetingVector = Quaternion.AngleAxis(45, Vector3.up)*targetingVector;
            TargetedPosition = cameraFocusPoint + targetingVector;

            IsFirePressed = Input.GetAxis("Fire1") > 0.5f;
        }

        public void OnDrawGizmos()
        {
            Debug.DrawLine(TargetedPosition,
                TargetedPosition + Vector3.up,
                Color.red);
        }
    }
}