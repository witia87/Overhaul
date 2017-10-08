using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Sight.SightBlockers
{
    public class SightBlocker : MonoBehaviour
    {
        protected SightStore SightStore;
        protected CameraStore CameraStore;
        protected virtual void Awake()
        {
            SightStore = FindObjectOfType<SightStore>();
            CameraStore = FindObjectOfType<CameraStore>();
        }
    }
}