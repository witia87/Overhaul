using UnityEngine;

namespace Assets.Modules
{
    public class Module : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Vector3 Size;

        protected Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                {
                    _rigidbody = GetComponent<Rigidbody>();
                }
                return _rigidbody;
            }
        }

        public virtual void Mount(GameObject parrentGameObject, Vector3 localPosition)
        {
            gameObject.transform.parent = parrentGameObject.transform;
            gameObject.transform.localPosition = localPosition;
            Rigidbody.isKinematic = true;
        }

        public virtual void Unmount()
        {
            gameObject.transform.parent = null;
            Rigidbody.isKinematic = false;
        }

        protected virtual void Awake()
        {
        }
    }
}