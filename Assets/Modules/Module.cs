using System.Collections.Generic;
using UnityEngine;

namespace Assets.Modules
{
    public abstract class Module : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Vector3 Size;

        public Rigidbody Rigidbody
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

        public Module ParrentModule { get; private set; }

        public bool HasParrentModule
        {
            get { return ParrentModule != null; }
        }

        public virtual void Mount(Module parrentGameObject, Vector3 localPosition)
        {
            ParrentModule = parrentGameObject;
            gameObject.transform.parent = parrentGameObject.transform;
            gameObject.transform.localPosition = localPosition;
            Rigidbody.isKinematic = true;
        }

        public virtual void Unmount()
        {
            gameObject.transform.parent = null;
            Rigidbody.isKinematic = false;
        }
        
        private List<Module> GetNeighboringModules(Module orderer)
        {
            var neighboringModules = new List<Module>(GetComponentsInChildren<Module>());
            if (HasParrentModule)
            {
                neighboringModules.Add(ParrentModule);
            }
            neighboringModules.Remove(orderer);
            var furtherModules = new List<Module>();
            foreach (var module in neighboringModules)
            {
                furtherModules.AddRange(module.GetNeighboringModules(this));
            }
            neighboringModules.AddRange(furtherModules);
            return neighboringModules;
        }
    }
}