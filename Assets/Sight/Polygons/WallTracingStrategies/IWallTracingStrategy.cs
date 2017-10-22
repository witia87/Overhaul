namespace Assets.Sight.Polygons.WallTracingStrategies
{
    public interface IWallTracingStrategy
    {
        IWallTracingStrategy GoToNextVertex(out MapVector vertex);
    }
}