using System;
using System.Collections.Generic;
using Nano.Engine.Graphics.Tileset;

namespace Nano.Engine
{
    public interface IMapGenerator
    {
        List<MapLayer> GenerateLayers();

        List<ITileset> GenerateTilesets();
    }
}

