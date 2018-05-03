using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class TorsoPresenter : ModulePresenter
    {
        [SerializeField] private LegsPresenter _legsPresenter;
        [SerializeField] private float _minimalRefreshTime = 0.05f;
        private float _timeSinceLastUpdate;

        public void UpdatePosition()
        {
            transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(Module.transform.position);
        }

        protected void Update()
        {
            base.Update();
            UpdatePosition();
            /*_timeSinceLastUpdate += Time.deltaTime;

            RecalculateAngles();
            _legsPresenter.RecalculateAngles();

            var currentLattitude = Module.Bottom.y;
            var positionInCameraPlane =
                CameraStore.Pixelation.TransformWorldPositionToCameraPlane(Module.Bottom);
            var newCameraPlaneX = Mathf.RoundToInt(positionInCameraPlane.x);
            var newCameraPlaneY = Mathf.RoundToInt(positionInCameraPlane.y);

            RecalculatePosition(newCameraPlaneX, newCameraPlaneY, currentLattitude);
            _legsPresenter.RecalculatePosition(newCameraPlaneX, newCameraPlaneY, currentLattitude);

            UpdatePosition();
            _legsPresenter.UpdatePosition();
            if (_timeSinceLastUpdate > _minimalRefreshTime &&
                (HaveAnglesChanged || _legsPresenter.HaveAnglesChanged))
            {
                _timeSinceLastUpdate = 0;
                UpdateAngles();
                _legsPresenter.UpdateAngles();
            }*/
        }
    }
}