using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Sight.SightBlockers
{
    public class SightBlocker : MonoBehaviour
    {
        protected SightStore SightStore;
        protected ICameraStore CameraStore;
        protected virtual void Awake()
        {
            SightStore = FindObjectOfType<SightStore>();
            CameraStore = FindObjectOfType<CameraComponent>();
        }
    }
}