using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class TorsoPresenter : ModulePresenter
    {
        [SerializeField] private Animator _animator;

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

        protected virtual void Update()
        {
            base.Update();
           // _animator.SetFloat("Speed", GetVelocity());
        }

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