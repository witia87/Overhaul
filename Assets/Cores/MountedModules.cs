using System.Collections.Generic;
using Assets.Modules.Artilleries;
using Assets.Modules.Movement;
using Assets.Modules.Turrets;

namespace Assets.Cores
{
    public class MountedModules
    {
        public List<IArtilleryControl> ArtilleryControls = new List<IArtilleryControl>();
        public IHumanoidMovementControl HumanoidMovementControl = null;
        public IVehicleMovementControl VehicleMovementControl = null;
        public List<ITurretControl> TurretControls = new List<ITurretControl>();


        public bool IsHumanoidMovementControlMounted
        {
            get { return HumanoidMovementControl != null; }
        }

        public bool IsVehicleMovementControlMounted
        {
            get { return VehicleMovementControl != null; }
        }

        public bool AreTurretControlsMounted
        {
            get { return TurretControls.Count > 0; }
        }

        public bool AreArtilleryControlsMounted
        {
            get { return ArtilleryControls.Count > 0; }
        }
    }
}