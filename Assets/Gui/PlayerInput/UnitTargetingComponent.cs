using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class UnitTargetingComponent : GuiComponent
    {
        [SerializeField] private LayerMask _emptyTargetingLayerMask;
        [SerializeField] private LayerMask _environmentLayerMask;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private Unit _unit;

        private void Update()
        {
            var ray =
                CameraStore.Pixelation.TransformCameraPlanePositionToWorldRay(MouseStore
                    .MousePositionInBoardSpace);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _targetLayerMask))
            {
                _unit.Control.LookAt(hit.point);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _environmentLayerMask))
            {
                _unit.Control.LookAt(hit.point);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _emptyTargetingLayerMask))
            {
                _unit.Control.LookAt(hit.point);
            }

            if (MouseStore.IsMousePressed)
            {
                _unit.Control.Fire();
            }
        }
    }
}