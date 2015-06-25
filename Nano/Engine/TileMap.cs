using System;
using System.Xml;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Nano.Engine.Cameras;
using Nano.Engine.Graphics.Tileset;
using Nano.Engine.Graphics;

namespace Nano.Engine
{
	public class TileMap
	{
		#region member data

        ISpriteManager m_SpriteManager;

		List<ITileset> m_Tilesets;
		List<MapLayer> m_MapLayers;

		int m_MapWidth;
		int m_MapHeight;

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
				return new Point( ((WidthInPixels / 2) - (TileWidth / 2)) ,TileHeight);
			}
		}
		#endregion

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

			for(int i = 1; i < layers.Count; i++)
			{
				if(m_MapWidth != m_MapLayers[i].Width || m_MapHeight != m_MapLayers[i].Height)
					throw new Exception("Map layers are not the same size");
			}
		}

		public void Draw (ICamera camera)
		{
            m_SpriteManager.StartBatch();

            if (TileMapType == TileMapType.Square) 
			{
                DrawSquareTileMap(camera);
            } 
            else if (TileMapType == TileMapType.Isometric) 
			{
                DrawIsometricTileMap();
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

        private void DrawIsometricTileMap()
        {
            Point min = new Point();
            Point max = new Point();

            //min.X = Math.Max (0, cameraPoint.X - 1);
            //min.Y = Math.Max (0, cameraPoint.Y - 1);
            //max.X = Math.Min (viewPoint.X + 1, m_MapWidth);
            //max.Y = Math.Min (viewPoint.Y + 1, m_MapHeight);

            min.X = 0;
            min.Y = 0;
            max.X = m_MapWidth;
            max.Y = m_MapHeight;

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

		public void AddLayer(MapLayer layer)
		{
			if(layer.Width != m_MapWidth && layer.Height != m_MapHeight)
				throw new Exception("Added map layer has incorrect size");

			m_MapLayers.Add(layer);
		}

		#region FromFile methods

        /// <summary>
        /// Loads the TileMap from the specified file.
        /// </summary>
        /// <returns>A fully initialised TileMap object, or null.</returns>
        /// <param name="filename">full path to the file including name</param>
        /// <param name="spriteManager">Sprite manager.</param>
		public static TileMap FromFile(string filename, ISpriteManager spriteManager)
		{
			TileMap map = null;

			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlNode root = doc.DocumentElement as XmlNode;

			if(root.Name == "map")
			{
				//XmlNode version = root.Attributes.GetNamedItem("version");
				//XmlNode orientation = root.Attributes.GetNamedItem("orientation");
				//XmlNode width = root.Attributes.GetNamedItem("width");
				//XmlNode height = root.Attributes.GetNamedItem("height");

                map = ParseMapFile(root, spriteManager);
			}

			return map;
		}

        private static TileMap ParseMapFile(XmlNode root, ISpriteManager spriteManager)
		{
            int? tileWidth = AttributeAsInt("tileWidth",root);
            int? tileHeight = AttributeAsInt("tileheight",root);

			List< Tuple<int,RegularTileset> > tempTilesets = new List<Tuple<int, RegularTileset>>();
			List<ITileset> tilesets = new List<ITileset>();
			List<MapLayer> layers = new List<MapLayer>();

			//first parse everything apart from the layer data.
			foreach(XmlNode n in root.ChildNodes)
			{
				if(n.Name == "properties")
				{
					ParseProperties(n);
				}
				else if (n.Name == "tileset")
				{
                    Tuple<int,RegularTileset> tilesetTuple = ParseTileset(n, spriteManager);
					if(tilesetTuple != null)
						tempTilesets.Add( tilesetTuple );
				}
				else if(n.Name == "objectgroup")
				{
					ParseObjectGroup(n);
				}
			}

			//second parse the layer data , this is a quick and dirty way of making sure all the tilesets are loaded
			foreach(XmlNode n in root.ChildNodes)
			{
				if(n.Name == "layer")
				{
					MapLayer layer = ParseLayer(n,tempTilesets);
					if(layer != null)
						layers.Add (layer);
				}
			}

			//assemble final map file
			foreach(Tuple<int,RegularTileset> pair in tempTilesets)
			{
				tilesets.Add( pair.Item2 );
			}
            TileMapType mapType = TileMapType.Square;
            return new TileMap(spriteManager, mapType, tileWidth.Value, tileHeight.Value,tilesets,layers);
		}

		private static void ParseProperties(XmlNode propNode)
		{
			//TODO Parse properties from tmx file
		}

        private static Tuple<int,RegularTileset> ParseTileset(XmlNode tilesetNode, ISpriteManager spriteManager)
		{
			// example xml input
			// <tileset firstgid="144" name="water" tilewidth="64" tileheight="64">
  			// <tileoffset x="0" y="32"/>
  			// <image source="grassland_water.png" width="1024" height="256"/>
 			// </tileset>

			int? firstgid = AttributeAsInt("firstgid",tilesetNode);
			string name = AttributeAsString("name",tilesetNode);
			int? tileWidth = AttributeAsInt("tilewidth",tilesetNode);
			int? tileHeight = AttributeAsInt("tileheight",tilesetNode);

			string textureName = null;
			int? textureWidth = null;
			int? textureHeight = null;
			int? xOffset = null;
			int? yOffset = null;

			//TODO make sure there is only one image child node
			foreach(XmlNode child in tilesetNode.ChildNodes)
			{
				if(child.Name == "image")
				{
					textureName = AttributeAsString("source",child);
					textureWidth = AttributeAsInt("width",child);
					textureHeight = AttributeAsInt("height", child);
				}
				else if(child.Name == "tileoffset")
				{
					xOffset = AttributeAsInt("x",child);
					yOffset = AttributeAsInt("y",child);
				}
			}

			//TODO validation of loaded data 	

			textureName = System.IO.Path.GetFileNameWithoutExtension(textureName);
            ITexture2D tilesetTexture = spriteManager.CreateTexture2D("tilesets/" + textureName);

			int tilesWide = textureWidth.Value / tileWidth.Value;
			int tilesHigh = textureHeight.Value / tileHeight.Value;

			RegularTileset tileset = new RegularTileset(name,tilesetTexture,tilesWide,tilesHigh,tileWidth.Value,tileHeight.Value);

			Point offset = new Point(xOffset.HasValue?xOffset.Value:0, yOffset.HasValue?yOffset.Value:0);
			tileset.Offset = offset;

			return new Tuple<int, RegularTileset>(firstgid.Value,tileset);
		}

		private static MapLayer ParseLayer(XmlNode layerNode,List< System.Tuple<int,RegularTileset> > tilesets )
		{
			// exmple xml input
			// <layer name="background" width="64" height="64">
  			// <data encoding="csv">

			string name = AttributeAsString("name",layerNode);
			int? width = AttributeAsInt("width",layerNode);
			int? height = AttributeAsInt("height",layerNode);

			//create empty layer
			MapLayer layer = new MapLayer(name, width.Value, height.Value);

			if(layerNode.FirstChild.Name == "data")
			{
				string encoding = AttributeAsString("encoding",layerNode.FirstChild);
				if(encoding == "csv")
				{
					string flatData = layerNode.FirstChild.InnerText;
					flatData = flatData.Replace("\n", String.Empty);
					string[] data =  flatData.Split(',');

					//parse the layer proper
					for(int y = 0; y < layer.Height; y++)
					{
						for(int x =0; x < layer.Width; x++)
						{
							string gidString = data[ y * layer.Width + x];
							Tuple<int,int> pair = GidToTuple(gidString,tilesets);
							TilesetTile tile = new TilesetTile(pair.Item1,pair.Item2);
							layer.SetTile(x,y,tile);
						}
					}
				}
			}

			return layer;
		}

		private static void ParseObjectGroup(XmlNode objGroupNode)
		{
			//TODO Parse object group from tmx file
		}

		private static int? AttributeAsInt(string name, XmlNode node)
		{
			XmlNode attrNode = node.Attributes.GetNamedItem(name);
			if( attrNode != null)
			{
				try
				{
					int result;
					int.TryParse(attrNode.Value,out result);
					return result;
				} 
				catch {}
			}
			
			return null;
		}

		private static string AttributeAsString(string name, XmlNode node)
		{
			XmlNode attrNode = node.Attributes.GetNamedItem(name);
			if( attrNode != null)
			{
				return attrNode.Value;
			}

			return null;
		}

		private static Tuple<int,int> GidToTuple(string gidString,List< Tuple<int,RegularTileset> > tilesets)
		{
			int id;
			int.TryParse(gidString, out id);

			if(id == 0)
				return new Tuple<int,int>(-1,-1);

			for(int i = tilesets.Count - 1; i >=0; i--)
			{
				Tuple<int,RegularTileset> pair = tilesets[i];
				if(id >= pair.Item1)
				{
					return new Tuple<int,int>(id - pair.Item1, i);
				}
			}

			return new Tuple<int,int>(0,0);
		}

		#endregion
	}
}

