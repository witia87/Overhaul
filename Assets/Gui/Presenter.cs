using Assets.MainCamera;
using UnityEngine;

namespace Assets.Gui
{
    public class Presenter : MonoBehaviour
    {
        protected ICameraStore CameraStore;

        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraComponent>();
        }

        protected virtual void Update()
        {
            gameObject.transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(gameObject.transform.position);
        }
    }
}