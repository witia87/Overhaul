using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class TorsoPresenter : LegsPresenter
    {
        [SerializeField] private LegsPresenter _legsPresenter;
        [SerializeField] private float _minimalRefreshTime = 0.05f;
        private float _timeSinceLastUpdate;

        protected void Update()
        {
            _timeSinceLastUpdate += Time.deltaTime;

            RecalculateAngles();
            _legsPresenter.RecalculateAngles();

            var currentLattitude = Module.Bottom.y;
            var positionInCameraPlane = CameraStore.Pixelation.TransformWorldPositionToCameraPlane(Module.Bottom);
            var newCameraPlaneX = Mathf.RoundToInt(positionInCameraPlane.x);
            var newCameraPlaneY = Mathf.RoundToInt(positionInCameraPlane.y);

            RecalculatePosition(newCameraPlaneX, newCameraPlaneY, currentLattitude);
            _legsPresenter.RecalculatePosition(newCameraPlaneX, newCameraPlaneY, currentLattitude);

            if (_timeSinceLastUpdate > _minimalRefreshTime &&
                (HasPositionChanged || HaveAnglesChanged) ||
                (_legsPresenter.HasPositionChanged || _legsPresenter.HaveAnglesChanged))
            {
                _timeSinceLastUpdate = 0;
                Refresh();
                _legsPresenter.Refresh();
            }
        }
    }
}