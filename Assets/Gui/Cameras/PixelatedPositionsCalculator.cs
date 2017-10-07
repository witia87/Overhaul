using UnityEngine;

namespace Assets.Gui.Cameras
{
    public class PixelatedPositionsCalculator
    {
        private readonly GameObject _cameraHook;
        private readonly GuiStore _guiStore;
        private readonly CameraComponent _camera;

        public PixelatedPositionsCalculator(GuiStore guiStore, GameObject cameraHook, CameraComponent camera)
        {
            _guiStore = guiStore;
            _cameraHook = cameraHook;
            _camera = camera;
        }

        public Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(position);
            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x * _guiStore.PixelsPerUnitInCameraSpace) /
                                        _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y * _guiStore.PixelsPerUnitInCameraSpace) /
                                        _guiStore.PixelsPerUnitInCameraSpace;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            var focusPointInCameraSpace = _cameraHook.transform.InverseTransformPoint(to - from);
            focusPointInCameraSpace.x = Mathf.Round(focusPointInCameraSpace.x * _guiStore.PixelsPerUnitInCameraSpace) /
                                        _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.y = Mathf.Round(focusPointInCameraSpace.y * _guiStore.PixelsPerUnitInCameraSpace) /
                                        _guiStore.PixelsPerUnitInCameraSpace;
            return _cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public Vector3 GetScreenPosition(Vector3 position)
        {
            var focusPointInCameraSpace = _camera.transform.InverseTransformPoint(position);
            focusPointInCameraSpace.x = focusPointInCameraSpace.x * _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.y = focusPointInCameraSpace.y * _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.z = 0;
            return focusPointInCameraSpace;
        }

        public Vector3 GetScreenOffset(Vector3 from, Vector3 to)
        {
            var focusPointInCameraSpace = _camera.transform.InverseTransformPoint(to - from);
            focusPointInCameraSpace.x = focusPointInCameraSpace.x * _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.y = focusPointInCameraSpace.y * _guiStore.PixelsPerUnitInCameraSpace;
            focusPointInCameraSpace.z = 0;
            return focusPointInCameraSpace;
        }
    }
}