using Assets.Maps;
using UnityEngine;

namespace Assets.Modules
{
    public abstract class Module : MonoBehaviour, IModuleControl
    {
        private Rigidbody _rigidbody;
        public float AngularDrag;
        public float Drag;
        public float Mass;
        public Vector3 Size;

        protected IMapStore MapStore;

        public IUnitControl Unit { get; private set; }

        public bool IsConntectedToUnit
        {
            get { return Unit != null; }
        }

        public Vector3 Center
        {
            get { return new Vector3(transform.position.x, transform.position.y + Size.y, transform.position.z); }
        }

        public Rigidbody Rigidbody
        {
            get { return IsConntectedToUnit ? Unit.Rigidbody : _rigidbody; }
        }

        protected virtual void Awake()
        {
            AttachRigidbody();
            MapStore = FindObjectOfType<MapStore>();
        }

        public virtual void Mount(GameObject parentGameObject, Vector3 localPosition)
        {
            transform.parent = parentGameObject.transform;
            transform.localPosition = localPosition;
            transform.localEulerAngles = Vector3.zero;
            Unit = transform.root.gameObject.GetComponent<Unit>();
            DestroyImmediate(_rigidbody);
            Unit.Rigidbody.mass += Mass;
        }

        public virtual void Unmount()
        {
            Unit.Rigidbody.mass -= Mass;
            transform.parent = null;
            Unit = null;
            AttachRigidbody();
        }

        private void AttachRigidbody()
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.mass = Mass;
            _rigidbody.drag = Drag;
            _rigidbody.angularDrag = AngularDrag;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}