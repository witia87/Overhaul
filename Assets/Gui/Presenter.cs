using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class Presenter : MonoBehaviour
    {
        protected CameraStore CameraStore;

        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
        }
    }
}