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
            MountedModule.Mount(_parrentModule, Position);
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

        private Module _parrentModule;
        private void OnValidate()
        {
            _parrentModule = GetComponent<Module>();
            if (IsModuleMounted)
            {
                MountedModule.Mount(_parrentModule, Position);
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(gameObject.transform.TransformPoint(Position), 0.1f);
        }
    }
}