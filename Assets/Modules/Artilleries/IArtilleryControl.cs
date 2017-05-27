namespace Assets.Modules.Artilleries
{
    public interface IArtilleryControl
    {
        float ChargeTime { get; }
        float ChargeTimeLeft { get; }
        void ChargeAndFire();
    }
}