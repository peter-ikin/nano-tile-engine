using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nano.Basics.Tree;
using Nano.Engine.Graphics.Tileset;
using Nano.Engine.Sys;

namespace Nano.Engine.Graphics.Map
{
    public class BspMapGenerator : IMapGenerator
    {
        #region constants

        //TODO : expose these as parameters
        const int MinHSize = 5;
        const int MinVSize = 5;

        #endregion

        MapLayer m_Layer;
        BinaryTree<Rectangle> m_Tree;
        IRandom m_Random;

        public BspMapGenerator(int mapWidth, int mapHeight, IRandom rng, int splitDepth)
        {
            m_Random = rng;
            Width = mapWidth;
            Height = mapHeight;

            Rectangle mapRect = new Rectangle(0, 0, Width, Height);

            m_Layer = new MapLayer("Bsp Generated Base Layer", Width, Height);
            m_Tree = new BinaryTree<Rectangle>();
            m_Tree.Root = Split(new BinaryTreeNode<Rectangle>(mapRect), splitDepth);
        }

        #region private properties

        private int Width{ get; set; }
        private int Height{ get; set; }

        #endregion

        #region IMapGenerator implementation

        public List<MapLayer> GenerateLayers()
        {
            var layerList = new List<MapLayer>();
            layerList.Add( m_Layer );
            return layerList;
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

        private Tuple<BinaryTreeNode<Rectangle>,BinaryTreeNode<Rectangle>> DoSplit(BinaryTreeNode<Rectangle> node)
        {
            Rectangle sourceRect = node.Value;

            Rectangle rect1;
            Rectangle rect2;

            if(m_Random.NextInt(0,1) == 0)
            {
                while(true)
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

                    //reject rectangles below specified ratio
                    float rect1WidthRatio = (float)rect1.Width / (float)rect1.Height;
                    float rect2WidthRatio = (float)rect2.Width / (float)rect2.Height;
                    if (rect1WidthRatio < 0.45 || rect2WidthRatio < 0.45) 
                        continue;

                    break;
                }
            }
            else
            {
                while (true)
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

                    //reject rectangles below specified ratio
                    float rect1HeightRatio = (float)rect1.Height / (float)rect1.Width;
                    float rect2HeightRatio = (float)rect2.Height / (float)rect2.Width;
                    if (rect1HeightRatio < 0.45 || rect2HeightRatio < 0.45) 
                        continue;

                    break;
                
                }
            }

            var first = new BinaryTreeNode<Rectangle>(rect1);
            var second = new BinaryTreeNode<Rectangle>(rect2);
            return new Tuple<BinaryTreeNode<Rectangle>,BinaryTreeNode<Rectangle>>(first, second);
        }
    }
}

