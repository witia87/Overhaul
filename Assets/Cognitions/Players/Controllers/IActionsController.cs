namespace Assets.Cognitions.Players.Controllers
{
    public interface IActionsController
    {
        bool IsDropWeaponPressed { get; }
        bool IsUsePressed { get; }
    }
}