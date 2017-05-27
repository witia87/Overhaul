using System;
using Assets.Cores.Slots;
using UnityEngine;

namespace Assets.Cores
{
    public class Core : MonoBehaviour
    {
        protected ArtillerySlot[] ArtillerySlots;
        
        protected HumanoidMovementSlot HumanoidMovementSlot;
        protected VehicleMovementSlot VehicleMovementSlot;
        protected TurretSlot[] TurretSlots;
        public CoreTypeIds Type = CoreTypeIds.Vehicle;
        
        public MountedModules MountedModules { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public void ScanModules()
        {
            MountedModules = new MountedModules();
            MountedModules.HumanoidMovementControl = HumanoidMovementSlot != null && HumanoidMovementSlot.IsModuleMounted
                ? HumanoidMovementSlot.MountedModule
                : null;

            MountedModules.VehicleMovementControl = VehicleMovementSlot != null && VehicleMovementSlot.IsModuleMounted
                ? VehicleMovementSlot.MountedModule
                : null;

            foreach (var turretSlot in TurretSlots)
            {
                if (turretSlot.IsModuleMounted)
                {
                    MountedModules.TurretControls.Add(turretSlot.MountedModule);
                }
            }

            foreach (var artillerySlot in ArtillerySlots)
            {
                if (artillerySlot.IsModuleMounted)
                {
                    MountedModules.ArtilleryControls.Add(artillerySlot.MountedModule);
                }
            }
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            var humanoidMovementSlots = gameObject.GetComponents<HumanoidMovementSlot>();
            if (humanoidMovementSlots.Length > 1)
            {
                throw new ApplicationException("Core cannot have multiple movement slots");
            }
            HumanoidMovementSlot = humanoidMovementSlots.Length == 1 ? humanoidMovementSlots[0] : null;

            var vehicleMovementSlots = gameObject.GetComponents<VehicleMovementSlot>();
            if (humanoidMovementSlots.Length > 1)
            {
                throw new ApplicationException("Core cannot have multiple movement slots");
            }
            VehicleMovementSlot = vehicleMovementSlots.Length == 1 ? vehicleMovementSlots[0] : null;

            TurretSlots = gameObject.GetComponents<TurretSlot>();
            ArtillerySlots = gameObject.GetComponents<ArtillerySlot>();
            ScanModules();
        }
    }
}