using Assets.Cognitions.Players;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class UnitTargetingComponent : GuiComponent
    {
        [SerializeField] private LayerMask _emptyTargetingLayerMask;
        [SerializeField] private LayerMask _environmentLayerMask;
        [SerializeField] private PlayerCognition _playerCognition;
        [SerializeField] private LayerMask _targetLayerMask;

        private void Update()
        {
            var ray =
                CameraStore.Pixelation.TransformCameraPlanePositionToWorldRay(MouseStore
                    .MousePositionInBoardSpace);
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