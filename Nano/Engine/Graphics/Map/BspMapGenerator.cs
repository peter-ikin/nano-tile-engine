using System;
using System.Collections.Generic;
using Nano.Engine.Graphics.Tileset;
using System.Runtime.Serialization.Formatters;
using Nano.Basics.Tree;
using Microsoft.Xna.Framework;

namespace Nano.Engine.Graphics.Map
{
    public class BspMapGenerator : IMapGenerator
    {
        MapLayer m_Layer;
        BinaryTree<Rectangle> m_Tree;

        public BspMapGenerator(int mapWidth, int mapHeight)
        {
            Rectangle mapRect = new Rectangle(0, 0, mapWidth, mapHeight);
            m_Layer = new MapLayer("Bsp Generated Base Layer", mapWidth, mapHeight);
            m_Tree = new BinaryTree<Rectangle>();
            m_Tree.Root = new BinaryTreeNode<Rectangle>(mapRect);
        }

        #region IMapGenerator implementation

        public List<MapLayer> GenerateLayers()
        {
            throw new NotImplementedException();
        }

        public List<ITileset> GenerateTilesets()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

