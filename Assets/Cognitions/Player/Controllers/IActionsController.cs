namespace Assets.Cognitions.Player.Controllers
{
    public interface IActionsController
    {
        bool IsDropWeaponPressed { get; }
        bool IsUsePressed { get; }
    }
}