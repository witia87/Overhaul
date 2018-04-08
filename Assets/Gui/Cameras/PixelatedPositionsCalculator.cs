using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class PixelatedPositionsCalculator
    {
        private readonly GameObject _cameraHook;

        private float _unitHeight;

        public PixelatedPositionsCalculator(CameraStore cameraStoreStore)
        {
            _cameraHook = cameraStoreStore.gameObject;
            _unitHeight = -_cameraHook.transform.forward.y;
        }

        public Vector2 TransformWorldPositionToCameraPlane(Vector3 worldPosition)
        {
            return _cameraHook.transform.InverseTransformPoint(worldPosition);
        }

        public Ray TransformCameraPlanePositionToWorldRay(Vector2 cameraPlanePosition)
        {
            var pointToCast = _cameraHook.transform.TransformPoint(cameraPlanePosition);
            var currentHeight = pointToCast.y;
            var castedPoint = pointToCast + _cameraHook.transform.forward *
                              (currentHeight - 10 /*fixed height*/) / _unitHeight;
            return new Ray(castedPoint, _cameraHook.transform.forward);
        }

        /// <summary>
        ///     Method casts a point from camera plane to the world space. Since such projection is a line (3D / 2D = 1D)
        ///     additional parameter is necesary - a lattitude above ground in world space
        /// </summary>
        /// <param name="cameraPlanePosition"></param>
        /// <param name="lattitude">Height above ground in world space</param>
        public Vector3 TransformCameraPlanePositionToWorldPosition(Vector2 cameraPlanePosition,
            float lattitude)
        {
            var pointToCast = _cameraHook.transform.TransformPoint(cameraPlanePosition);
            var currentHeight = pointToCast.y;
            var castedPoint = pointToCast +
                              _cameraHook.transform.forward * (currentHeight - lattitude) / _unitHeight;
            return castedPoint;
        }

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(position);

            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x);
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y);

            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(to - from);
            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x);
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y);
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }
    }
}