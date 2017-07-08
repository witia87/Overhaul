using Assets.Gui.MainCamera;
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