using UnityEngine;

namespace Assets.Sight.SightBlockers
{
    public class SightBlocker : MonoBehaviour
    {
        protected SightStore SightStore;
        protected virtual void Awake()
        {
            SightStore = FindObjectOfType<SightStore>();
        }
    }
}