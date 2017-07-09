using System;

namespace Assets.Modules.Artilleries
{
    public class ArtilleryModule : Module, IArtilleryControl
    {
        public float ChargeTime { get; set; }
        public float ChargeTimeLeft { get; set; }

        public void ChargeAndFire()
        {
            throw new NotImplementedException();
        }
    }
}