using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureAcresData;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

namespace AzureAcres
{
    public class TileEngine : ActionableObject
    {
        public delegate void OnContainerOpenedHandler(object sender);
        public event OnContainerOpenedHandler OnContainerOpened;

        private int _windowWidth;
        public int WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }
        private int _windowHeight;
        public int WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }
        private ContentManager _contentManager;
        public ContentManager ContentManager
        {
            get { return _contentManager; }
            set { _contentManager = value; }
        }
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }
        private int _offsetX;
        public int OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; }
        }
        private int _offsetY;
        public int OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; }
        }
        private Hashtable _crops;
        public Hashtable Crops
        {
            get { return _crops; }
            set { _crops = value; }
        }

        public TileEngine(ContentManager contentManager)
        { _contentManager = contentManager; OffsetX = 0; OffsetY = 0; _crops = new Hashtable(); }

        #region UPDATING
        public void Update(GameTime gameTime)
        {
            CheckMapBounds();
            ScrollMap();
            UpdateAnimatedTiles(gameTime);
            UpdateCrops();

            #region PORTALS
            Portal portal = GetCollidingPortal();
            if (portal != null)
            {
                ChangeMap(portal.ToMapName);
                Portal dest = Map.Portals.First<Portal>(p => p.Name == portal.ToPortalName);
                /// Direction vector
                Direction dir = DirectionHelper.DirectionFromString(dest.Direction);
                PlayerPosition.FacingDirection = dir;
                Point destinationCoors = dest.Coordinates;
                switch (dir)
                {
                    case Direction.North:
                        destinationCoors.Y -= PlayerPosition.Height;
                        break;
                    case Direction.South:
                        /// TODO: change this from 32.
                        destinationCoors.Y += 32;
                        break;
                    case Direction.West:
                        destinationCoors.X -= PlayerPosition.Width;
                        break;
                    case Direction.East:
                        destinationCoors.X += 32;
                        break;
                }
                PlayerPosition.SetMapPosition(destinationCoors);
            }
            #endregion

            /// If the player is colliding with the fringe layer
            /// then push them back to where they were
            if (PlayerColliding())
            {
                PlayerPosition.MapPosition = PlayerPosition.PreviousMapPosition;
                PlayerPosition.ScreenPosition = PlayerPosition.PreviousScreenPosition;
            }
        }
        public void UpdateAnimatedTiles(GameTime gameTime)
        {
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (AnimatedTile tile in Map.AnimatedTiles)
            {
                tile.Sprite.UpdateAnimation(elapsedSeconds);
            }
        }
        public void UpdateCrops()
        {
            foreach (string mapName in Crops.Keys)
                foreach (Crop c in (Crops[mapName] as List<Crop>))
                    c.Update(Clock.ElapsedMilliseconds);
        }
        public void UpdateContainers()
        {
            foreach (ItemContainer container in Map.Containers)
            {
                if (!container.IsVisible)
                    continue;
            }
        }
        #endregion

        #region DRAWING
        public void Draw(SpriteBatch spriteBatch, int layer)
        {
            spriteBatch.Begin();
            if (layer == 0) DrawLayer(spriteBatch, Map.BaseLayer);
            if (layer == 1) DrawLayer(spriteBatch, Map.SecondBaseLayer);
            if (layer == 3) DrawLayer(spriteBatch, Map.ObjectLayer);
            if (layer == 2) DrawLayer(spriteBatch, Map.FringeLayer);

            spriteBatch.End();
        }
        public void DrawLayer(SpriteBatch spriteBatch, int[] layer)
        {
            Rectangle dest = new Rectangle(0, 0, 32, 32);
            Rectangle src = new Rectangle(0, 0, 32, 32);

            for (int y = 0; y < Map.MapHeight; ++y)
            {
                for (int x = 0; x < Map.MapWidth; ++x)
                {
                    int textureTileIndex = layer[x + y * Map.MapWidth];
                    if (textureTileIndex == -1) continue;
                    src.X = 0;
                    src.Y = 0;

                    if (textureTileIndex != 0)
                    {
                        int textureWidthInTiles = Map.Texture.Width / Map.TileWidth;
                        src.X = (textureTileIndex % textureWidthInTiles) * Map.TileWidth;
                        src.Y = (int)Math.Floor(1.0 * textureTileIndex / textureWidthInTiles) * 32;
                    }

                    dest.X = x * 32 + OffsetX;
                    dest.Y = y * 32 + OffsetY;
                    spriteBatch.Draw(Map.Texture, dest, src, Color.White);
                }
            }
        }
        public void DrawBaseLayers(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            DrawBaseLayers(spriteBatch, _map.BaseLayer, _map.SecondBaseLayer);
            spriteBatch.End();
        }
        public void DrawBaseLayers(SpriteBatch spriteBatch, int[] layer1, int[] layer2)
        {
            Rectangle dest = new Rectangle(0, 0, 32, 32);
            Rectangle src = new Rectangle(0, 0, 32, 32);

            for (int y = 0; y < Map.MapHeight; ++y)
            {
                for (int x = 0; x < Map.MapWidth; ++x)
                {
                    int textureTileIndex = layer1[x + y * Map.MapWidth];
                    int textureTileIndex2 = layer2[x + y * Map.MapWidth];
                    if (textureTileIndex == -1 &&
                        textureTileIndex2 == -1) continue;

                    if (textureTileIndex != -1)
                    {
                        /// layer 1
                        src.X = 0;
                        src.Y = 0;
                        if (textureTileIndex != 0)
                        {
                            int textureWidthInTiles = Map.Texture.Width / Map.TileWidth;
                            src.X = (textureTileIndex % textureWidthInTiles) * Map.TileWidth;
                            src.Y = (int)Math.Floor(1.0 * textureTileIndex / textureWidthInTiles) * 32;
                        }
                        dest.X = x * 32 + OffsetX;
                        dest.Y = y * 32 + OffsetY;
                        spriteBatch.Draw(Map.Texture, dest, src, Color.White);
                    }

                    if (textureTileIndex2 != -1)
                    {
                        /// layer 2
                        src.X = 0;
                        src.Y = 0;
                        if (textureTileIndex2 != 0)
                        {
                            int textureWidthInTiles = Map.Texture.Width / Map.TileWidth;
                            src.X = (textureTileIndex2 % textureWidthInTiles) * Map.TileWidth;
                            src.Y = (int)Math.Floor(1.0 * textureTileIndex2 / textureWidthInTiles) * 32;
                        }
                        dest.X = x * 32 + OffsetX;
                        dest.Y = y * 32 + OffsetY;
                        spriteBatch.Draw(Map.Texture, dest, src, Color.White);
                    }
                }
            }
        }
        public void DrawAnimatedTiles(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Vector2 v = new Vector2(OffsetX, OffsetY);
            foreach (AnimatedTile tile in Map.AnimatedTiles)
            {
                tile.Sprite.PlayAnimation(tile.Sprite.Animations[0]);
                tile.Sprite.Draw(spriteBatch, tile.Coordinates + v, 1);
            }
            spriteBatch.End();
        }
        public void DrawCrops(SpriteBatch spriteBatch, int startY, int endY)
        {
            if (Crops.ContainsKey(Map.Name))
            {
                foreach (Crop c in (Crops[Map.Name] as List<Crop>))
                {
                    if (c.Coordinates.Y >= startY &&
                        c.Coordinates.Y <= endY)
                        DrawCrop(spriteBatch, c);
                }
            }
        }
        public void DrawContainers(SpriteBatch spriteBatch, Vector2 position)
        {
            foreach (ItemContainer container in Map.Containers)
            {
                if (!container.IsVisible)
                    continue;
                spriteBatch.Draw(container.BackgroundTexture, Util.CreateRectangle(position, container.BackgroundTexture), Color.White);
                break;
            }
        }
        private void DrawCrop(SpriteBatch spriteBatch, Crop crop)
        {
            spriteBatch.Begin();
            Vector2 v = new Vector2(OffsetX, OffsetY);
            if (crop.Dimensions.Y > 32) v.Y -= (crop.Dimensions.Y - 32);
            if (crop.Dimensions.X > 32) v.X -= (crop.Dimensions.X - 32) / 2;
            spriteBatch.Draw(crop.Texture, crop.Coordinates + v, new Rectangle((crop.CurrentStage - 1) * crop.Dimensions.X, 0, crop.Dimensions.X, crop.Dimensions.Y), Color.White);
            spriteBatch.End();
        }
        #endregion

        #region COLLISION DETECTION
        private void CheckMapBounds()
        {
            if (PlayerPosition.MapPosition.X <= 0)
                PlayerPosition.MapPosition.X = 0;
            if (PlayerPosition.MapPosition.X + PlayerPosition.Width >= Map.MapWidthInPixels)
                PlayerPosition.MapPosition.X = Map.MapWidthInPixels - PlayerPosition.Width;
            if (PlayerPosition.MapPosition.Y <= 0)
                PlayerPosition.MapPosition.Y = 0;
            if (PlayerPosition.MapPosition.Y + PlayerPosition.Height >= Map.MapHeightInPixels)
                PlayerPosition.MapPosition.Y = Map.MapHeightInPixels - PlayerPosition.Height;
        }
        private bool PlayerColliding()
        {
            int xGrid = (PlayerPosition.MapPosition.X == 0) ? 0 : PlayerPosition.MapPosition.X - (PlayerPosition.MapPosition.X % Map.TileWidth);
            int yGrid = (PlayerPosition.MapPosition.Y == 0) ? 0 : PlayerPosition.MapPosition.Y - (PlayerPosition.MapPosition.Y % Map.TileHeight);
            int xIndex = (xGrid == 0) ? 0 : (xGrid / Map.TileWidth);
            int yIndex = ((yGrid / Map.TileHeight) * Map.MapWidth);
            int topLeftIndex = xIndex + yIndex;
            bool colliding = false;
            try
            {
                /// TODO: fix bug for index++ on the farthest x and y tiles
                /// right now it's just catching the exception and saying it hit 
                /// something so that it will be pushed back where it was
                colliding = (
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid, yGrid, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex] == 1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid + Map.TileWidth, yGrid, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + 1] == 1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + Map.MapWidth] == 1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid + Map.TileWidth, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + 1 + Map.MapWidth] == 1)
                    ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid, yGrid, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex] != -1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid + Map.TileWidth, yGrid, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + 1] != -1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + Map.MapWidth] != -1) ||
                    (Colliding(PlayerPosition.BoundingBox, new Rectangle(xGrid + Map.TileWidth, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + 1 + Map.MapWidth] != -1)
                    );

                if (Crops.ContainsKey(Map.Name))
                {
                    foreach (Crop c in (List<Crop>)Crops[Map.Name])
                    {
                        if (Colliding(PlayerPosition.BoundingBox, c.BoundingBox))
                        { colliding = true; break; }
                    }
                }
            }
            catch (Exception ex) { colliding = true; }
            return colliding;
        }

        public Vector2 GetCollidingTileVector2(Vector2 position)
        {
            int xGrid = (position.X == 0) ? 0 : (int)position.X - ((int)position.X % Map.TileWidth);
            int yGrid = (position.Y == 0) ? 0 : (int)position.Y - ((int)position.Y % Map.TileHeight);
            return new Vector2(xGrid, yGrid);
        }
        public Rectangle GetCollidingTileRect(Vector2 position)
        {
            int xGrid = (position.X == 0) ? 0 : (int)position.X - ((int)position.X % Map.TileWidth);
            int yGrid = (position.Y == 0) ? 0 : (int)position.Y - ((int)position.Y % Map.TileHeight);
            return new Rectangle(xGrid, yGrid, Map.TileWidth, Map.TileHeight);
        }
        public bool Colliding(Rectangle rect)
        {
            int xGrid = (rect.X == 0) ? 0 : rect.X - (rect.X % Map.TileWidth);
            int yGrid = (rect.Y == 0) ? 0 : rect.Y - (rect.Y % Map.TileHeight);
            int xIndex = (xGrid == 0) ? 0 : (xGrid / Map.TileWidth);
            int yIndex = ((yGrid / Map.TileHeight) * Map.MapWidth);
            int topLeftIndex = xIndex + yIndex;
            bool colliding = false;
            try
            {
                colliding = (
                    (Colliding(rect, new Rectangle(xGrid, yGrid, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex] == 1) ||
                    (Colliding(rect, new Rectangle(xGrid + Map.TileWidth, yGrid, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + 1] == 1) ||
                    (Colliding(rect, new Rectangle(xGrid, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + Map.MapWidth] == 1) ||
                    (Colliding(rect, new Rectangle(xGrid + Map.TileWidth, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.CollisionLayer[topLeftIndex + 1 + Map.MapWidth] == 1)
                    ||
                    (Colliding(rect, new Rectangle(xGrid, yGrid, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex] != -1) ||
                    (Colliding(rect, new Rectangle(xGrid + Map.TileWidth, yGrid, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + 1] != -1) ||
                    (Colliding(rect, new Rectangle(xGrid, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + Map.MapWidth] != -1) ||
                    (Colliding(rect, new Rectangle(xGrid + Map.TileWidth, yGrid + Map.TileHeight, Map.TileWidth, Map.TileHeight)) && Map.ObjectLayer[topLeftIndex + 1 + Map.MapWidth] != -1)
                    );
                if (Crops.ContainsKey(Map.Name))
                {
                    foreach (Crop c in (List<Crop>)Crops[Map.Name])
                    {
                        if (Colliding(rect, c.BoundingBox))
                        { colliding = true; break; }
                    }
                }
            }
            catch (Exception ex) { colliding = true; }
            return colliding;
        }
        public bool Colliding(Rectangle object1, Vector2 object2)
        {
            Rectangle o2 = new Rectangle((int)object2.X, (int)object2.Y, 1, 1);
            return Colliding(object1, o2);
        }
        public bool Colliding(Rectangle object1, Rectangle object2)
        {
            if ((object1.Y + object1.Height) < object2.Y) return false;
            if (object1.Y > (object2.Y + object2.Height)) return false;
            if ((object1.X + object1.Width) < object2.X) return false;
            if (object1.X > (object2.X + object2.Width)) return false;
            return true;
        }
        #endregion

        #region CROPS
        public void AddCrop(string mapName, Crop _crop)
        {
            if (!Crops.ContainsKey(mapName))
                Crops[mapName] = new List<Crop>();
            if (!(Crops[mapName] as List<Crop>).Exists(c => c.Coordinates.X == _crop.Coordinates.X && c.Coordinates.Y == _crop.Coordinates.Y))
                (Crops[mapName] as List<Crop>).Add(_crop);
        }
        public void RemoveCrop(string mapName, int hashCode)
        {
            if (!Crops.ContainsKey(mapName))
                return;
            Crop c = (Crops[mapName] as List<Crop>).FirstOrDefault(cr => cr.GetHashCode().ToString() == hashCode.ToString());
            if (c != null)
                (Crops[mapName] as List<Crop>).Remove(c);
        }
        public Crop GetCrop(string mapName, Vector2 location)
        {
            if (!Crops.ContainsKey(mapName)) return null;
            Vector2 tileVector = GetCollidingTileVector2(location);
            return
                (Crops[mapName] as List<Crop>).FirstOrDefault(c => c.Coordinates == tileVector);

        }
        #endregion

        #region TILES
        public int GetTileIndexAtLocation(int layer, Vector2 location)
        {
            int xGrid = (location.X == 0) ? 0 : (int)location.X - ((int)location.X % Map.TileWidth);
            int yGrid = (location.Y == 0) ? 0 : (int)location.Y - ((int)location.Y % Map.TileHeight);
            int xIndex = (xGrid == 0) ? 0 : (xGrid / Map.TileWidth);
            int yIndex = ((yGrid / Map.TileHeight) * Map.MapWidth);
            int index = xIndex + yIndex;
            return index;
        }
        public int GetTileValueAtLocation(int layer, Vector2 location)
        {
            int index = GetTileIndexAtLocation(layer, location);
            switch (layer)
            {
                case 0:
                    return Map.BaseLayer[index];
                case 1:
                    return Map.FringeLayer[index];
                case 2:
                    return Map.CollisionLayer[index];
            }
            return -1;
        }
        #endregion

        #region ANIMATED TILES

        #endregion

        #region CONTAINERS
        private ItemContainer GetCollidingContainer()
        {
            foreach (ItemContainer p in Map.Containers)
                if (Colliding(new Rectangle((int)p.Position.X, (int)p.Position.Y, (int)p.Dimensions.X, (int)p.Dimensions.Y),
                    PlayerPosition.SelectionMapVector))
                    return p;
            return null;
        }
        #endregion

        #region PORTALS
        private Portal GetCollidingPortal()
        {
            foreach (Portal p in Map.Portals)
                if (Colliding(PlayerPosition.BoundingBox, new Rectangle(p.Coordinates.X, p.Coordinates.Y, 32, 32)))
                    return p;
            return null;
        }
        #endregion

        #region MAP
        public void ChangeMap(string mapName)
        {
            Map = ContentManager.Load<Map>(mapName);
        }

        private void ScrollMap()
        {
            if (PlayerPosition.MapPosition.X + PlayerPosition.Width / 2 < WindowWidth / 2)
                OffsetX = 0;
            else if (PlayerPosition.MapPosition.X + PlayerPosition.Width / 2 > Map.MapWidthInPixels - WindowWidth / 2)
                OffsetX = -1 * (Map.MapWidthInPixels - WindowWidth);
            else if (PlayerPosition.MapPosition.X + PlayerPosition.Width / 2 > WindowWidth / 2)
                OffsetX = WindowWidth / 2 - (PlayerPosition.MapPosition.X + PlayerPosition.Width / 2);

            if (PlayerPosition.MapPosition.Y + PlayerPosition.Height / 2 < WindowHeight / 2)
                OffsetY = 0;
            else if (PlayerPosition.MapPosition.Y + PlayerPosition.Height / 2 > Map.MapHeightInPixels - WindowHeight / 2)
                OffsetY = -1 * (Map.MapHeightInPixels - WindowHeight);
            else if (PlayerPosition.MapPosition.Y + PlayerPosition.Height / 2 > WindowHeight / 2)
                OffsetY = WindowHeight / 2 - (PlayerPosition.MapPosition.Y + PlayerPosition.Height / 2);

            if (WindowWidth > Map.MapWidthInPixels)
                OffsetX = (WindowWidth - Map.MapWidthInPixels) / 2;
            if (WindowHeight > Map.MapHeightInPixels)
                OffsetY = (WindowHeight - Map.MapHeightInPixels) / 2;

            PlayerPosition.ScreenPosition.X = PlayerPosition.MapPosition.X + OffsetX;
            PlayerPosition.ScreenPosition.Y = PlayerPosition.MapPosition.Y + OffsetY;
        }
        #endregion

        #region COORDS
        public Vector2 ToScreenCoords(Vector2 v)
        {
            return v + new Vector2(OffsetX, OffsetY);
        }
        public Vector2 ToMapCoords(Vector2 v)
        {
            return v - new Vector2(OffsetX, OffsetY);
        }
        public Rectangle ToScreenCoords(Rectangle r)
        {
            Vector2 v = new Vector2(r.X, r.Y);
            Vector2 v2 = ToScreenCoords(v);
            r.X = (int)v2.X;
            r.Y = (int)v2.Y;
            return r;
        }
        public Rectangle ToMapCoords(Rectangle r)
        {
            Vector2 v = new Vector2(r.X, r.Y);
            Vector2 v2 = ToMapCoords(v);
            r.X = (int)v2.X;
            r.Y = (int)v2.Y;
            return r;
        }
        #endregion

        #region UNLOAD
        public void Unload()
        {
            Map.Texture.Dispose();
        }
        #endregion

        #region ACTING LOGIC
        public override void Act(ContentObject actingObject, Character actingCharacter, Vector2 location)
        {
            base.Act(actingObject, actingCharacter, location);
            Crop crop = GetCrop(Map.Name, location);
            int tileIndex = GetTileIndexAtLocation(0, location);
            int tileIndexValue = GetTileValueAtLocation(0, location);

            #region CONTAINERS
            /// open the container and then return
            ItemContainer container = GetCollidingContainer();
            if (container != null &&
                OnContainerOpened != null)
            { OnContainerOpened(container); return; }
            #endregion

            /// IF THERE IS A CROP THERE
            if (crop != null)
            {
                if (crop.IsGrown)
                {
                    Session.AddInventory(crop);
                    RemoveCrop(_map.Name, crop.GetHashCode());
                    actingCharacter.State = Character.CharacterState.Idle;
                }
                else if (actingObject is Tool)
                {
                    Tool tool = (actingObject as Tool);
                    switch (tool.Type)
                    {
                        case "WateringCan":
                            crop.IsWatered = true;
                            Map.BaseLayer[tileIndex] = (int)TILES.DARKDIRT;
                            /// remove the current handler because we want to have more
                            /// time for the watering :P
                            crop.OnNoLongerWatered -= new Crop.OnNoLongerWateredHandler(crop_OnNoLongerWatered);
                            crop.OnNoLongerWatered += new Crop.OnNoLongerWateredHandler(crop_OnNoLongerWatered);
                            break;
                    }
                }
            }
            /// THERE IS NO CROP
            else
            {
                if (actingObject is Tool)
                {
                    Tool tool = (actingObject as Tool);
                    switch (tool.Type)
                    {
                        case "Hoe":
                            if (tileIndexValue == (int)TILES.LIGHTGRASS || tileIndexValue == (int)TILES.MEDIUMGRASS || tileIndexValue == (int)TILES.DARKGRASS)
                                Map.BaseLayer[tileIndex] = (int)TILES.LIGHTDIRT;
                            break;
                    }
                }
                else if (actingObject is Crop)
                {
                    if (tileIndexValue == (int)TILES.LIGHTDIRT || tileIndexValue == (int)TILES.DARKDIRT)
                    {
                        Crop c = actingObject as Crop;
                        c.Coordinates = GetCollidingTileVector2(location);
                        AddCrop(Map.Name, c);
                        Session.RemoveInventory(c);
                        Session.SelectedInventoryItem = null;
                    }
                }
            }
        }

        /// <summary>
        /// When a crop is finished being watered then 
        /// Change the color of the tile back
        /// </summary>
        /// <param name="sender"></param>
        private void crop_OnNoLongerWatered(object sender)
        {
            Crop crop = (sender as Crop);
            int tileIndex = GetTileIndexAtLocation(0, crop.Coordinates);
            Map.BaseLayer[tileIndex] = (int)TILES.LIGHTDIRT;
        }
        #endregion

        public enum TILES
        {
            LIGHTGRASS = 0,
            MEDIUMGRASS = 1,
            DARKGRASS = 2,
            LIGHTDIRT = 32,
            DARKDIRT = 33,
            CHESTLEFT = 171,
            CHESTRIGHT = 172,
            REFRIDGERATOR = 208,
        }
    }
}
