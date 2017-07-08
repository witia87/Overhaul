using System;
using UnityEngine;

namespace Assets.Modules
{
    public abstract class Slot<TMountableModule> : MonoBehaviour
        where TMountableModule : Module
    {
        public TMountableModule MountedModule;
        public Vector3 Position;

        public bool IsModuleMounted
        {
            get { return MountedModule != null; }
        }

        public event Action ModuleHasBeenMounted;
        public event Action ModuleHasBeenUnmounted;

        public virtual void MountModule(TMountableModule module)
        {
            if (IsModuleMounted)
                throw new ApplicationException("Module already mounted onto the MovementModule.");
            MountedModule = module;
            MountedModule.Mount(gameObject, Position);
            if (ModuleHasBeenMounted != null) ModuleHasBeenMounted();
        }

        public virtual void UnmountModule()
        {
            if (!IsModuleMounted)
                throw new ApplicationException("Module is not mounted onto the MovementModule.");
            MountedModule.Unmount();
            MountedModule = null;
            if (ModuleHasBeenUnmounted != null) ModuleHasBeenUnmounted();
        }

        private void Start()
        {
            if (IsModuleMounted)
            {
                MountedModule.Mount(gameObject, Position);
            }
        }

        private void OnValidate()
        {
            if (IsModuleMounted)
            {
                MountedModule.transform.parent = gameObject.transform;
                MountedModule.transform.localPosition = Position;
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(gameObject.transform.TransformPoint(Position), 0.02f);
        }
    }
}