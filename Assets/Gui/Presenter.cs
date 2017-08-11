using Assets.Gui.Cameras;
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
    }
}