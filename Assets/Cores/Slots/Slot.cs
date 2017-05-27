using System;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cores.Slots
{
    public class Slot<TMountableModule> : MonoBehaviour
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

        private void OnValidate()
        {
            if (IsModuleMounted)
            {
                MountedModule.Mount(gameObject, Position);
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(gameObject.transform.TransformPoint(Position), 0.1f);
        }
    }
}