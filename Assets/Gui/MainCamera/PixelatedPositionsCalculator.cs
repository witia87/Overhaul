using UnityEngine;

namespace Assets.Gui.MainCamera
{
    public class PixelatedPositionsCalculator
    {
        private readonly GameObject _cameraHook;
        private readonly CameraComponent _parent;

        public PixelatedPositionsCalculator(CameraComponent parent, GameObject cameraHook)
        {
            _parent = parent;
            _cameraHook = cameraHook;
        }

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(position);
            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x*_parent.PixelsPerUnit)/
                                        _parent.PixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y*_parent.PixelsPerUnit)/
                                        _parent.PixelsPerUnit;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(to - from);
            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x*_parent.PixelsPerUnit)/
                                        _parent.PixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y*_parent.PixelsPerUnit)/
                                        _parent.PixelsPerUnit;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }
    }
}