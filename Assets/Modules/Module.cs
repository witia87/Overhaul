using Assets.Editor.Modules;
using Assets.Maps;
using UnityEngine;

namespace Assets.Modules
{
    public abstract class Module : MonoBehaviour, IModuleControl
    {
        private Rigidbody _rigidbody;
        public float AngularDrag;
        public float Drag;

        protected IMapStore MapStore;
        public float Mass;
        public Vector3 Size;

        public IUnitControl Unit { get; private set; }

        protected MeshCollider MeshCollider { get; private set; }
        protected Mesh InitialMesh { get; private set; }

        protected MeshHelper MeshHelper;

        public bool IsConntectedToUnit
        {
            get { return Unit != null; }
        }

        public Rigidbody Rigidbody
        {
            get { return IsConntectedToUnit ? Unit.Rigidbody : _rigidbody; }
        }

        public Vector3 Center
        {
            get { return new Vector3(transform.position.x, transform.position.y + Size.y, transform.position.z); }
        }

        protected virtual void Awake()
        {
            AttachRigidbody();
            MapStore = FindObjectOfType<MapStore>();
            MeshCollider = GetComponent<MeshCollider>();
            MeshHelper = new MeshHelper(MeshCollider.sharedMesh);
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