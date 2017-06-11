using UnityEngine;

namespace Assets.MainCamera
{
    public class PixelatedPositionsCalculator
    {
        private readonly CameraComponent _parrent;
        private readonly GameObject _cameraHook;

        public PixelatedPositionsCalculator(CameraComponent parrent, GameObject cameraHook)
        {
            _parrent = parrent;
            _cameraHook = cameraHook;
        }

        /* private Vector3 GetCameraPosition(Vector3 focusPoint)
         {
             var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(focusPoint);
             focusPointInCameraSpace.z = 0;
             focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x* PixelsPerUnit)/
                                         PixelsPerUnit;
             focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y* PixelsPerUnit) /
                                         PixelsPerUnit;
             return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
         }*/

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(position);
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x*_parrent.PixelsPerUnit)/
                                        _parrent.PixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y*_parrent.PixelsPerUnit)/
                                        _parrent.PixelsPerUnit;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(to - from);
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x*_parrent.PixelsPerUnit)/
                                        _parrent.PixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y*_parrent.PixelsPerUnit)/
                                        _parrent.PixelsPerUnit;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }
    }
}