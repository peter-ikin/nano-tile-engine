using System;
using Nano.Engine.Graphics;
using System.Xml;
using Nano.Engine.Graphics.Tileset;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nano.Engine.Graphics.Map;

namespace Nano.Engine.IO.Import
{
    /// <summary>
    /// File importer for tiled .tmx files
    /// </summary>
    public class TmxFileImporter
    {
        public TileMap TileMap { get; set; }

        public TmxFileImporter(string filename, ISpriteManager spriteManager)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode root = doc.DocumentElement as XmlNode;

            if(root.Name == "map")
            {
                //XmlNode version = root.Attributes.GetNamedItem("version");
                //XmlNode orientation = root.Attributes.GetNamedItem("orientation");
                //XmlNode width = root.Attributes.GetNamedItem("width");
                //XmlNode height = root.Attributes.GetNamedItem("height");

                TileMap = ParseMapFile(root, spriteManager);
            }
        }

        private TileMap ParseMapFile(XmlNode root, ISpriteManager spriteManager)
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
    }
}

