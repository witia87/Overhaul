namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public interface IActionsController
    {
        bool IsDropWeaponPressed { get; }
        bool IsUsePressed { get; }
    }
}