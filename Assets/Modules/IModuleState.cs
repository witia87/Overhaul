namespace Assets.Modules
{
    public interface IModuleState<out TStateIds>
    {
        TStateIds Update();
        void FixedUpdate();
    }
}