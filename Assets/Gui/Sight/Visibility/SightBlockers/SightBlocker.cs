using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui.Sight.Visibility.SightBlockers
{
    public class SightBlocker : MonoBehaviour
    {
        protected CameraStore CameraStore;
        protected SightStore SightStore;

        protected virtual void Awake()
        {
            SightStore = FindObjectOfType<SightStore>();
            CameraStore = FindObjectOfType<CameraStore>();
        }
    }
}