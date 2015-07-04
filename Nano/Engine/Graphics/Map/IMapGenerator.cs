using System;
using System.Collections.Generic;
using Nano.Engine.Graphics.Tileset;

namespace Nano.Engine.Graphics.Map
{
    public interface IMapGenerator
    {
        List<MapLayer> GenerateLayers();

        List<ITileset> GenerateTilesets();
    }
}

