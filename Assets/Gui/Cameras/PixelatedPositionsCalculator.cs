using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class PixelatedPositionsCalculator
    {
        private readonly GameObject _cameraHook;

        public PixelatedPositionsCalculator(CameraStore cameraStoreStore)
        {
            _cameraHook = cameraStoreStore.gameObject;
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