using System;
using System.Collections.Generic;
using Nano.Engine.Graphics.Tileset;
using System.Runtime.Serialization.Formatters;
using Nano.Basics.Tree;
using Microsoft.Xna.Framework;
using Nano.Engine.Sys;
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;

namespace Nano.Engine.Graphics.Map
{
    public class BspMapGenerator : IMapGenerator
    {
        MapLayer m_Layer;
        BinaryTree<Rectangle> m_Tree;
        IRandom m_Random;

        public BspMapGenerator(int mapWidth, int mapHeight, IRandom rng)
        {
            m_Random = rng;

            Rectangle mapRect = new Rectangle(0, 0, mapWidth, mapHeight);

            m_Layer = new MapLayer("Bsp Generated Base Layer", mapWidth, mapHeight);
            m_Tree = new BinaryTree<Rectangle>();
            m_Tree.Root = Split(new BinaryTreeNode<Rectangle>(mapRect), 1);
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

        private BinaryTreeNode<Rectangle> Split(BinaryTreeNode<Rectangle> node, int numIterations)
        {
            while(numIterations > 0)
            {
                numIterations--;

                var results = DoSplit(node);
                node.Left = Split(results.Item1, numIterations);
                node.Right = Split(results.Item2, numIterations);
            }

            return node;
        }

        Tuple<BinaryTreeNode<Rectangle>,BinaryTreeNode<Rectangle>> DoSplit(BinaryTreeNode<Rectangle> node)
        {
            Rectangle sourceRect = node.Value;

            Rectangle rect1;
            Rectangle rect2;

            if(m_Random.NextInt(0,1) == 0)
            {
                //split vertically
                rect1 = new Rectangle(
                    node.Value.X, 
                    node.Value.Y,
                    m_Random.NextInt(1, node.Value.Width),
                    node.Value.Height);

                rect2 = new Rectangle(
                    node.Value.X + rect1.Width, 
                    node.Value.Y,
                    node.Value.Width - rect1.Width,
                    node.Value.Height);
            }
            else
            {
                //split horizontally
                rect1 = new Rectangle(
                    node.Value.X, 
                    node.Value.Y,
                    node.Value.Width,
                    m_Random.NextInt(1, node.Value.Height));

                rect2 = new Rectangle(
                    node.Value.X, 
                    node.Value.Y + rect1.Height,
                    node.Value.Width,
                    node.Value.Height - rect1.Height);
            }

            var first = new BinaryTreeNode<Rectangle>(rect1);
            var second = new BinaryTreeNode<Rectangle>(rect2);
            return new Tuple<BinaryTreeNode<Rectangle>,BinaryTreeNode<Rectangle>>(first, second);
        }
    }
}

