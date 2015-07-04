using System;
using System.Xml;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Nano.Engine.Cameras;
using Nano.Engine.Graphics.Tileset;
using Nano.Engine.Graphics;
using Nano.Engine.IO.Import;
using System.IO;

namespace Nano.Engine.Graphics.Map
{
    public class TileMap
    {
        #region member data

        ISpriteManager m_SpriteManager;

        List<ITileset> m_Tilesets;
        List<MapLayer> m_MapLayers;

        int m_MapWidth;
        int m_MapHeight;
        Point m_Origin;

        int m_TileHeight;
        int m_TileWidth;
        TileMapType m_TileMapType;

        #endregion

        #region properties

        public int TileHeight 
        {
            get { return m_TileHeight; }
        }

        public int TileWidth 
        {
            get { return m_TileWidth; }
        }

        public TileMapType TileMapType 
        {
            get { return m_TileMapType; }
        }

        public int WidthInPixels
        {
            get { return m_MapWidth * TileWidth; }
        }

        public int HeightInPixels
        {
            get { return m_MapHeight * TileHeight; }
        }

        public Point Origin
        {
            get 
            {
                return m_Origin;
            }
        }
        #endregion

        #region Constructors

        public TileMap (ISpriteManager spriteManager, TileMapType mapType, int tileWidth, int tileHeight, List<ITileset> tilesets, List<MapLayer> layers)
        {
            m_SpriteManager = spriteManager;

            m_TileMapType = mapType;
            m_TileHeight = tileHeight;
            m_TileWidth = tileWidth;

            m_Tilesets = tilesets;
            m_MapLayers = layers;

            m_MapWidth = m_MapLayers[0].Width;
            m_MapHeight = m_MapLayers[0].Height;

            VerifyLayerGeometry(m_MapLayers);

            SetOrigin(m_TileMapType);
        }

        public TileMap(ISpriteManager spriteManager, TileMapType mapType, int tileWidth, int tileHeight, IMapGenerator generator)
        {
            m_SpriteManager = spriteManager;

            var generatedLayers = generator.GenerateLayers();
            m_MapLayers = generatedLayers;

            m_TileMapType = mapType;
            m_TileHeight = tileHeight;
            m_TileWidth =  tileWidth;

            m_MapWidth = m_MapLayers[0].Width;
            m_MapHeight = m_MapLayers[0].Height;

            VerifyLayerGeometry(m_MapLayers);

            var generatedTilesets = generator.GenerateTilesets();
            m_Tilesets = generatedTilesets;

            SetOrigin(m_TileMapType);
        }

        #endregion

        private void VerifyLayerGeometry(List<MapLayer> layers)
        {
            for (int i = 1; i < layers.Count; i++)
            {
                if (m_MapWidth != m_MapLayers[i].Width || m_MapHeight != m_MapLayers[i].Height)
                    throw new Exception("Map layers are not the same size");
            }
        }

        private void SetOrigin(TileMapType mapType)
        {
            if (mapType == TileMapType.Square)
                m_Origin = new Point(0, 0);
            else
                if (mapType == TileMapType.Isometric)
                    m_Origin = new Point(((WidthInPixels / 2) - (TileWidth / 2)), TileHeight);
        }

        /// <summary>
        /// Draw the TileMap using the supplied Camera
        /// </summary>
        /// <param name="camera">Camera.</param>
        public void Draw (ICamera camera)
        {
            m_SpriteManager.StartBatch(camera.Transformation);

            if (TileMapType == TileMapType.Square) 
            {
                DrawSquareTileMap(camera);
            } 
            else if (TileMapType == TileMapType.Isometric) 
            {
                DrawIsometricTileMap(camera);
            }

            m_SpriteManager.EndBatch();
        }

        private void DrawSquareTileMap(ICamera camera)
        {
            Point cameraPoint = VectorToCell(camera.Position * (1 / camera.Zoom));
            Point viewPoint = VectorToCell(new Vector2((camera.Position.X + camera.ViewportRectangle.Width) * (1 / camera.Zoom), (camera.Position.Y + camera.ViewportRectangle.Height) * (1 / camera.Zoom)));

            Point min = new Point();
            Point max = new Point();

            min.X = Math.Max(0, cameraPoint.X - 1);
            min.Y = Math.Max(0, cameraPoint.Y - 1);
            max.X = Math.Min(viewPoint.X + 1, m_MapWidth);
            max.Y = Math.Min(viewPoint.Y + 1, m_MapHeight);

            Rectangle destination = new Rectangle(0, 0, TileWidth, TileHeight);
            TilesetTile tile;

            foreach (MapLayer layer in m_MapLayers)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    destination.Y = y * TileHeight;
                    for (int x = min.X; x < max.X; x++)
                    {
                        tile = layer.GetTile(x, y);
                        if (tile.TileIndex == -1 || tile.TilesetId == -1)
                            continue;

                        destination.X = x * TileWidth;

                        m_SpriteManager.DrawTexture2D(m_Tilesets[tile.TilesetId].Texture, destination, m_Tilesets[tile.TilesetId].Bounds[tile.TileIndex]);
                    }
                }
            }
        }

        private void DrawIsometricTileMap(ICamera camera)
        {
            Point cameraPoint = VectorToCell(camera.Position * (1 / camera.Zoom));
            Point viewPoint = VectorToCell(new Vector2((camera.Position.X + camera.ViewportRectangle.Width) * (1 / camera.Zoom), (camera.Position.Y + camera.ViewportRectangle.Height) * (1 / camera.Zoom)));

            Point min = new Point();
            Point max = new Point();

            min.X = 0;//Math.Max(0, cameraPoint.X - 1);
            min.Y = 0;//Math.Max(0, cameraPoint.Y - 1);
            max.X = m_MapWidth; //Math.Min(viewPoint.X + 1, m_MapWidth);
            max.Y = m_MapHeight; //Math.Min(viewPoint.Y + 1, m_MapHeight);

            Rectangle destination = new Rectangle(0, 0, TileWidth, TileHeight);
            TilesetTile tile;

            foreach (MapLayer layer in m_MapLayers)
            {
                for (int i = min.X; i < max.X; i++)
                {
                    for (int j = min.Y; j < max.Y; j++)
                    {
                        tile = layer.GetTile(i, j);
                        if (tile.TileIndex == -1 || tile.TilesetId == -1)
                            continue;

                        Point rowAdjustment = new Point(j * -(TileWidth / 2), j * (TileHeight / 2));
                        ITileset tset = m_Tilesets[tile.TilesetId];

                        destination.X = Origin.X + rowAdjustment.X + (i * (TileWidth / 2)) + tset.Offset.X;
                        destination.Y = Origin.Y + rowAdjustment.Y + (i * (TileHeight / 2)) - (tset.Bounds[tile.TileIndex].Height - TileHeight) + tset.Offset.Y;

                        destination.Height = m_Tilesets[tile.TilesetId].Bounds[tile.TileIndex].Height;
                        destination.Width = m_Tilesets[tile.TilesetId].Bounds[tile.TileIndex].Width;

                        m_SpriteManager.DrawTexture2D(m_Tilesets[tile.TilesetId].Texture, destination, m_Tilesets[tile.TilesetId].Bounds[tile.TileIndex]);
                    }
                }
            }
        }

        public Point VectorToCell(int x, int y)
        {
            return VectorToCell(new Vector2((float)x,(float)y));
        }

        public Point VectorToCell(Vector2 position)
        {
            if(TileMapType == TileMapType.Square)
            {
                return new Point((int)position.X / TileWidth, (int)position.Y / TileHeight);
            }
            else
            {
                double tw, th, tx, ty;

                tw = TileWidth;
                th = TileHeight;

                tx = Math.Round(( (position.X - Origin.X) / tw) + (position.Y / th));
                ty = Math.Round(( (position.X - Origin.X) / tw) + (position.Y / th));

                return new Point((int)tx,(int)ty);
            }
        }

        /// <summary>
        /// Adds a layer to the TileMap.
        /// Added layers must have the same dimensions as existing layers
        /// </summary>
        /// <param name="layer">The Layer to add</param>
        public void AddLayer(MapLayer layer)
        {
            if(layer.Width != m_MapWidth && layer.Height != m_MapHeight)
                throw new Exception("Added map layer has incorrect size");

            m_MapLayers.Add(layer);
        }
            
        /// <summary>
        /// Loads the TileMap from the specified file.
        /// Supported file formats : .tmx(partial)
        /// </summary>
        /// <returns>A fully initialised TileMap object, or null.</returns>
        /// <param name="filename">full path to the file including name</param>
        /// <param name="spriteManager">Sprite manager.</param>
        public static TileMap FromFile(string filename, ISpriteManager spriteManager)
        {
            string ext = Path.GetExtension(filename);
            if (ext == ".tmx")
            {
                TmxFileImporter importer = new TmxFileImporter(filename, spriteManager);
                return importer.TileMap;
            }

            throw new FileLoadException(string.Format("Unsupported file type {0}", ext));
        }
    }
}

