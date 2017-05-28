using Assets.Flux;
using UnityEngine;

namespace Assets.Camera
{
    public class CameraComponent : MonoBehaviour
    {
        private ICameraStore _cameraStore;

        public GameObject FocusObject;

        private void Awake()
        {
            _cameraStore = GameMechanics.Stores.CameraStore;
            GameMechanics.Dispatcher.Dispatch(Commands.InitializeCamera,
                new InitializeCameraPayload { CameraEulerAngles = gameObject.transform.eulerAngles });
        }

        void Update() {
            var focusObjectPosition = FocusObject.transform.position;
            this.transform.SetPositionAndRotation(focusObjectPosition + new Vector3(0,1,0),this.transform.rotation);
            GameMechanics.Dispatcher.Dispatch(Commands.SetCameraFocusPoint, new SetCameraFocusPointPayload() { FocusPoint = focusObjectPosition });
        }
    }
}