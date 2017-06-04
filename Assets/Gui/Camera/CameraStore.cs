using Assets.Flux;
using Assets.Flux.Stores;
using UnityEngine;

namespace Assets.Presentation.Camera
{
    public class CameraStore : MonoBehaviour
    {
        private static CameraComponent _instance;

        public GameObject FocusObject;
        public int PixelationSize = 2;
        public float PixelationsPixelsPerUnit = 64;

        public static float PixelsPerUnit
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraComponent>();
                }
                return _instance.PixelationsPixelsPerUnit;
            }
        }

        public static Vector3 CameraEulerAngles
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraComponent>();
                }
                return _instance.gameObject.transform.localEulerAngles;
            }
        }

        public bool IsInitialized { get; private set; }

        public Vector3 FocusPoint { get; private set; }


        private Vector3 GetCameraPosition(Vector3 focusPoint)
        {
            var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(focusPoint);
            focusPointInCameraSpace.z = 0;
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x * PixelationsPixelsPerUnit) /
                                        PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y * PixelationsPixelsPerUnit) /
                                        PixelationsPixelsPerUnit;
            return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public static Vector3 GetClosestPixelatedPosition(Vector3 position)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraComponent>();
            }
            var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(position);
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }

        public static Vector3 GetPixelatedOffset(Vector3 from, Vector3 to)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraComponent>();
            }
            var focusPointInCameraSpace = _instance._cameraHook.transform.InverseTransformPoint(to - from);
            //focusPointInCameraSpace.z = 0; // ortogonalProjectionToTheCameraPlane
            focusPointInCameraSpace.x = Mathf.Floor(focusPointInCameraSpace.x * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            focusPointInCameraSpace.y = Mathf.Floor(focusPointInCameraSpace.y * _instance.PixelationsPixelsPerUnit) /
                                        _instance.PixelationsPixelsPerUnit;
            return _instance._cameraHook.transform.TransformPoint(focusPointInCameraSpace);
        }
    }
}