using UnityEngine;

namespace Assets.Modules
{
    public abstract class Module : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public float AngularDrag;
        public float Drag;
        public float Mass;
        public Vector3 Size;

        public Unit Unit { get; private set; }

        public bool IsConntectedToUnit
        {
            get { return Unit != null; }
        }

        public Rigidbody Rigidbody
        {
            get { return IsConntectedToUnit ? Unit.Rigidbody : _rigidbody; }
        }

        public virtual void Awake()
        {
            AttachRigidbody();
        }

        public virtual void Mount(GameObject parentGameObject, Vector3 localPosition)
        {
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.transform.localPosition = localPosition;
            Unit = gameObject.transform.root.gameObject.GetComponent<Unit>();
            DestroyImmediate(_rigidbody);
            Unit.Rigidbody.mass += Mass;
        }

        public virtual void Unmount()
        {
            Unit.Rigidbody.mass -= Mass;
            gameObject.transform.parent = null;
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