using Assets.Flux;
using UnityEngine;

namespace Assets.Camera
{
    public class CameraComponent : MonoBehaviour
    {
        private ICameraStore _cameraStore;

        private void Awake()
        {
            _cameraStore = GameMechanics.Stores.CameraStore;
            GameMechanics.Dispatcher.Dispatch(Commands.InitializeCamera,
                new InitializeCameraPayload {CameraEulerAngles = gameObject.transform.eulerAngles});
        }
    }
}