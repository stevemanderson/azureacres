using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AzureAcresData
{
    public class Map : ContentObject
    {
        private int[] _mapDimensions;
        public int[] MapDimensions
        {
            get { return _mapDimensions; }
            set { _mapDimensions = value; }
        }

        [ContentSerializerIgnore]
        public int MapWidth
        {
            get { return MapDimensions[0]; }
        }
        [ContentSerializerIgnore]
        public int MapHeight
        {
            get { return MapDimensions[1]; }
        }

        private int[] _tileDimensions;
        public int[] TileDimensions
        {
            get { return _tileDimensions; }
            set { _tileDimensions = value; }
        }

        [ContentSerializerIgnore]
        public int MapWidthInPixels { get { return MapWidth * TileWidth; } }
        [ContentSerializerIgnore]
        public int MapHeightInPixels { get { return MapHeight * TileHeight; } }

        [ContentSerializerIgnore]
        public int TileWidth
        {
            get { return TileDimensions[0]; }
        }
        [ContentSerializerIgnore]
        public int TileHeight
        {
            get { return TileDimensions[1]; }
        }

        private string _textureName;
        public string TextureName
        {
            get { return _textureName; }
            set { _textureName = value; }
        }

        private Texture2D _texture;
        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private int[] _baseLayer;
        public int[] BaseLayer
        {
            get { return _baseLayer; }
            set { _baseLayer = value; }
        }

        private int[] _secondBaseLayer;
        public int[] SecondBaseLayer
        {
            get { return _secondBaseLayer; }
            set { _secondBaseLayer = value; }
        }
        
        private int[] _fringeLayer;
        public int[] FringeLayer
        {
            get { return _fringeLayer; }
            set { _fringeLayer = value; }
        }
        private int[] _objectLayer;
        public int[] ObjectLayer
        {
            get { return _objectLayer; }
            set { _objectLayer = value; }
        } 
        private int[] _collisionLayer;
        public int[] CollisionLayer
        {
            get { return _collisionLayer; }
            set { _collisionLayer = value; }
        }


        private string[] _animatedTileLayer;
        public string[] AnimatedTileLayer
        {
            get { return _animatedTileLayer; }
            set { _animatedTileLayer = value; }
        }

        private List<AnimatedTile> _animatedTiles;
        [ContentSerializerIgnore]
        public List<AnimatedTile> AnimatedTiles
        {
            get { return _animatedTiles; }
            set { _animatedTiles = value; }
        }

        private List<Portal> _portals;
        public List<Portal> Portals
        {
            get { return _portals; }
            set { _portals = value; }
        }

        private List<ItemContainer> _containers;
        public List<ItemContainer> Containers
        {
            get { return _containers; }
            set { _containers = value; }
        }

        public Map() { AnimatedTiles = new List<AnimatedTile>(); }

        public class MapContentReader : ContentTypeReader<Map>
        {
            protected override Map Read(ContentReader input, Map existingInstance)
            {
                Map existing = existingInstance;
                if (existing == null)
                    existing = new Map();
                existing.Name = input.ReadString();
                existing.MapDimensions = input.ReadObject<int[]>();
                existing.TileDimensions = input.ReadObject<int[]>();
                existing.TextureName = input.ReadString();
                existing.Texture = input.ContentManager.Load<Texture2D>(existing.TextureName);
                existing.BaseLayer = input.ReadObject<int[]>();
                existing.SecondBaseLayer = input.ReadObject<int[]>();
                existing.FringeLayer = input.ReadObject<int[]>();
                existing.ObjectLayer = input.ReadObject<int[]>();
                existing.CollisionLayer = input.ReadObject<int[]>();
                existing.AnimatedTileLayer = input.ReadObject<string[]>();
                for(int i = 0; i < existing.AnimatedTileLayer.Count(); ++i)
                {
                    if (String.IsNullOrEmpty(existing.AnimatedTileLayer[i]))
                        continue;
                    int x = (i == 0) ? 0 : i % existing.MapWidth;
                    int y = (int)Math.Floor(1.0*i / existing.MapWidth);
                    AnimatedTile tile = (input.ContentManager.Load<AnimatedTile>(existing.AnimatedTileLayer[i]) as AnimatedTile).Clone() as AnimatedTile;
                    tile.Coordinates = new Vector2(x*existing.TileWidth, y*existing.TileHeight);
                    existing.AnimatedTiles.Add(tile);
                }
                existing.Portals = input.ReadObject<List<Portal>>();
                existing.Containers = input.ReadObject<List<ItemContainer>>();
                return existing;
            }
        }
    }
}
