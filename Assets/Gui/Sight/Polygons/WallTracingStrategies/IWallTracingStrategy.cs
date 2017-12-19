namespace Assets.Gui.Sight.Polygons.WallTracingStrategies
{
    public interface IWallTracingStrategy
    {
        IWallTracingStrategy GoToNextVertex(out MapVector vertex);
    }
}