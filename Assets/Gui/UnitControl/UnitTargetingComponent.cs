using Assets.Cognitions.Players;
using UnityEngine;

namespace Assets.Gui.UnitControl
{
    public class UnitTargetingComponent : GuiComponent
    {
        [SerializeField] private LayerMask _emptyTargetingLayerMask;
        [SerializeField] private LayerMask _environmentLayerMask;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private PlayerCognition _playerCognition;

        private void Update()
        {
            var ray =
                CameraStore.TransformCameraPlanePositionToWorldRay(MouseStore.MousePositionInBoardSpace);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _targetLayerMask))
            {
                _playerCognition.LookAt(hit.point);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _environmentLayerMask))
            {
                _playerCognition.LookAt(hit.point);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, _emptyTargetingLayerMask))
            {
                _playerCognition.LookAt(hit.point);
            }

            if (MouseStore.IsMousePressed)
            {
                _playerCognition.Fire();
            }
        }
    }
}