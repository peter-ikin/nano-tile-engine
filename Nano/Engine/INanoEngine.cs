namespace Nano.Engine
{
    public interface INanoEngine
    {
        TileEngineType TileEngineType { get; }

        int TileHeight { get; }

        int TileWidth { get; }
    }
}

