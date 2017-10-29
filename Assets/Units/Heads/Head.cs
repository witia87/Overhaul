using Assets.Units.Heads.Vision;
using Assets.Vision;
using UnityEngine;

namespace Assets.Units.Heads
{
    public class Head : MonoBehaviour
    {
        [SerializeField] private Vector3 _visionPosition;

        public Vector3 VisionPosition
        {
            get { return transform.TransformPoint(_visionPosition); }
        }

        private VisionStore _visionStore;
        public IVisionObserver VisionObserver;
        private void Awake()
        {
            _visionStore = FindObjectOfType<VisionStore>();
            VisionObserver = _visionStore.RegisterUnit(transform.root.GetComponent<Unit>());
        }
    }
}