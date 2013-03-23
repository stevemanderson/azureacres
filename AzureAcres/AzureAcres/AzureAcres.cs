using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AzureAcresData;

namespace AzureAcres
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AzureAcres : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileEngine _tileEngine;
        TopInventory _topInventory;
        Inventory _inventory;
        ClockAndWeather _clockAndWeather;
        Texture2D _overlay;

        float _elapsedTime = 0.0f;
        int _frameCounter = 0;
        int _fps = 0;

        Texture2D _selectorBox;

        bool _drawSelectedTile = false;
        Texture2D _selectedTile;

        Character player;

        public AzureAcres()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            Content.RootDirectory = "Content";
        }

        protected void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _tileEngine.WindowWidth = Window.ClientBounds.Width;
            _tileEngine.WindowHeight = Window.ClientBounds.Height;
            _topInventory.Position = new Vector2(_tileEngine.WindowWidth / 2 - _topInventory.Width / 2, 0);
            _inventory.Position = new Vector2(_tileEngine.WindowWidth / 2 - _inventory.Width / 2, 100);
            _clockAndWeather.Position = new Vector2(_tileEngine.WindowWidth - _clockAndWeather.Width, 0);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileEngine = new TileEngine(Content);
            _tileEngine.WindowWidth = Window.ClientBounds.Width;
            _tileEngine.WindowHeight = Window.ClientBounds.Height;
            _tileEngine.ChangeMap("Maps/Map001");
            _tileEngine.OnContainerOpened += new TileEngine.OnContainerOpenedHandler(_tileEngine_OnContainerOpened);

            _topInventory = new TopInventory(Content);
            _topInventory.Position = new Vector2(_tileEngine.WindowWidth / 2 - _topInventory.Width / 2, 0);
            _inventory = new Inventory(Content);
            _inventory.Position = new Vector2(_tileEngine.WindowWidth / 2 - _inventory.Width / 2, 100);
            _inventory.TopInventory = _topInventory;

            _selectorBox = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _selectorBox.SetData(new Color[] { Color.Black });

            _selectedTile = Util.CreateRectangle(GraphicsDevice, 32, 32, Color.White);

            player = Content.Load<Character>("Characters/GirlFarmer");
            Session.AddInventory(Content.Load<Tool>("Tools/Hoe"));
            Session.AddInventory(Content.Load<Tool>("Tools/WateringCan"));
            Session.AddInventory(Content.Load<Crop>("Crops/Pumpkin").Clone() as Crop);
            Session.AddInventory(Content.Load<Crop>("Crops/StrawberryBush").Clone() as Crop);
            Session.SelectedInventoryItem = Session.TopInventory[0];
            Fonts.LoadContent(Content);

            PlayerPosition.Width = 32;
            PlayerPosition.Height = 32;
            PlayerPosition.MapPosition = new Point(300, 300);
            PlayerPosition.ScreenPosition = new Point(300, 300);

            Character testingNCP = Content.Load<Character>("Characters/TestingNPC").Clone() as Character;
            testingNCP.MapPosition = new Point(320, 320);
            testingNCP.DestinationPosition = testingNCP.MapPosition;
            Session.NPCCharacters.Add((testingNCP.Clone() as Character));

            _clockAndWeather = new ClockAndWeather(graphics.GraphicsDevice, Content);
            _clockAndWeather.Position = new Vector2(_tileEngine.WindowWidth - _clockAndWeather.Width, 0);

            _overlay = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _overlay.SetData(new Color[] { Color.DarkBlue });
        }

        protected void _tileEngine_OnContainerOpened(object sender)
        {
            player.State = Character.CharacterState.Idle;
            _inventory.Visible = true;
            (sender as ItemContainer).IsVisible = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Fonts.UnloadContent();
            _selectorBox.Dispose();
            player.MapSprite.Texture.Dispose();
            player.WalkingSprite.Texture.Dispose();
            _tileEngine.Unload();
            _overlay.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Clock.Update(gameTime);

            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter++;
            if (_elapsedTime > 1)
            {
                _fps = _frameCounter;
                _frameCounter = 0;
                _elapsedTime = 0;
            }

            // Allows the game to exit
            if (InputManager.IsActionTriggered(InputManager.Action.ExitGame))
                this.Exit();

            InputManager.Update();

            #region HANDLE GAME
            if (!_inventory.Visible)
            {
                if (InputManager.IsActionTriggered(InputManager.Action.MainMenu))
                    _inventory.OpenInventory();
                Vector2 move = Vector2.Zero;

                /// If the player isn't doing something else allow
                /// movement
                if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterLeft)) move = new Vector2(-3, 0);
                else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterRight)) move = new Vector2(3, 0);
                else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterUp)) move = new Vector2(0, -3);
                else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterDown)) move = new Vector2(0, 3);

                if (player.State == Character.CharacterState.Idle ||
                    player.State == Character.CharacterState.Walking)
                    PlayerPosition.Move(move);
                else
                {
                    if (Session.IsSelectedItemTool)
                        if ((Session.SelectedInventoryItem as Tool).IsPlaybackComplete) player.State = Character.CharacterState.Idle;
                }

                /// selected tile
                //_drawSelectedTile = InputManager.IsActionPressed(InputManager.Action.MainMenu);

#if DEBUG
                if (InputManager.IsKeyPressed(Keys.OemTilde)) Clock.RealTimeRatio = 3600;
                else Clock.RealTimeRatio = 240;
#endif

                if (player.State != Character.CharacterState.Acting)
                {
                    if (InputManager.IsKeyTriggered(Keys.RightShift)) Session.SelectNextInventoryItem();
                    if (InputManager.IsKeyTriggered(Keys.LeftShift)) Session.SelectPreviousInventoryItem();
                }

                if (InputManager.IsActionTriggered(InputManager.Action.Ok))
                {
                    if (Session.SelectedInventoryItem != null)
                    {
                        player.State = Character.CharacterState.Acting;
                        if (Session.SelectedInventoryItem != null && Session.SelectedInventoryItem is Tool)
                            (Session.SelectedInventoryItem as Tool).ResetAnimation();
                        else if (Session.IsSelectedItemCrop)
                            player.State = Character.CharacterState.Idle;
                        /// Do the act
                        /// thinking that you can send the tool to the object and the tilemanager
                        /// with the location that the action is done, this will allow the tilemanager to decide
                        /// what is to be done and the object
                        _tileEngine.Act((ContentObject)Session.SelectedInventoryItem, player, PlayerPosition.SelectionMapVector);
                    }
                }
            }
            #endregion
            else
            {
                _tileEngine.UpdateContainers();
                /// handle the menu
                _inventory.HandleInput();
            }

            _tileEngine.Update(gameTime);

            foreach (Character npc in Session.NPCCharacters)
            {
                if (npc.State == Character.CharacterState.Idle)
                {
                    Random r = new Random((int)DateTime.Now.Ticks);
                    int directionRandom = r.Next(1, 100);
                    if (directionRandom < 5)
                    {
                        npc.Direction = (Direction)r.Next(0, 4);
                        npc.State = Character.CharacterState.Walking;
                        npc.DestinationPosition = npc.MapPosition;
                        switch (npc.Direction)
                        {
                            case Direction.North:
                                npc.DestinationPosition = new Point(npc.MapPosition.X, npc.MapPosition.Y - 32);
                                break;
                            case Direction.South:
                                npc.DestinationPosition = new Point(npc.MapPosition.X, npc.MapPosition.Y + 32);
                                break;
                            case Direction.East:
                                npc.DestinationPosition = new Point(npc.MapPosition.X + 32, npc.MapPosition.Y);
                                break;
                            case Direction.West:
                                npc.DestinationPosition = new Point(npc.MapPosition.X - 32, npc.MapPosition.Y);
                                break;
                        }
                    }
                }
                if (npc.DestinationPosition == npc.MapPosition) npc.State = Character.CharacterState.Idle;
                /// else move the npc
                else
                {
                    Vector2 dir = new Vector2(npc.DestinationPosition.X - npc.MapPosition.X, npc.DestinationPosition.Y - npc.MapPosition.Y);
                    if (dir.X < 0) npc.MapPosition = new Point(npc.MapPosition.X - 1, npc.MapPosition.Y);
                    if (dir.X > 0) npc.MapPosition = new Point(npc.MapPosition.X + 1, npc.MapPosition.Y);
                    if (dir.Y < 0) npc.MapPosition = new Point(npc.MapPosition.X, npc.MapPosition.Y - 1);
                    if (dir.Y > 0) npc.MapPosition = new Point(npc.MapPosition.X, npc.MapPosition.Y + 1);
                    if (_tileEngine.Colliding(new Rectangle((int)npc.DestinationPosition.X, (int)npc.DestinationPosition.Y, 32, 32)) ||
                        _tileEngine.Colliding(new Rectangle((int)npc.DestinationPosition.X, (int)npc.DestinationPosition.Y, 32, 32), PlayerPosition.BoundingBox))
                    { npc.DestinationPosition = npc.MapPosition; }
                }

                /// Check if colliding with the player
                if (_tileEngine.Colliding(PlayerPosition.BoundingBox, new Rectangle((int)npc.DestinationPosition.X, (int)npc.DestinationPosition.Y, 32, 32)))
                {
                    PlayerPosition.MapPosition = PlayerPosition.PreviousMapPosition;
                    PlayerPosition.ScreenPosition = PlayerPosition.PreviousScreenPosition;
                    player.State = Character.CharacterState.Idle;
                    npc.State = Character.CharacterState.Idle;
                }
            }

            //// update the overlay
            _overlay.SetData(new Color[] { Color.Black });

            HandleInventory();
            base.Update(gameTime);
        }

        protected void HandleInventory()
        {
            /// INVENTORY
            /// 
            if (!_inventory.Visible)
                _topInventory.SelectedIndex = Session.SelectedInventoryItemIndex;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// Some of the items like the players, characters and the crops should all be changed to be drawables
        /// so that they can be sorted for drawing
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(Color.Black);
            _tileEngine.DrawBaseLayers(spriteBatch);
            _tileEngine.Draw(spriteBatch, 3);

            spriteBatch.Begin();
            if (_drawSelectedTile)
            {
                Rectangle selectRect = _tileEngine.GetCollidingTileRect(PlayerPosition.SelectionMapVector);
                spriteBatch.Draw(_selectedTile, _tileEngine.ToScreenCoords(selectRect), Color.White);
            }
            spriteBatch.End();

            _tileEngine.DrawAnimatedTiles(spriteBatch);
            _tileEngine.DrawCrops(spriteBatch, 0, PlayerPosition.MapPosition.Y);

            #region PLAYER
            spriteBatch.Begin();
            player.ResetAnimation(PlayerPosition.IsMoving);
            player.Direction = PlayerPosition.FacingDirection;
            switch (player.State)
            {
                case Character.CharacterState.Idle:
                    if (player.MapSprite != null)
                    {
                        player.MapSprite.UpdateAnimation(elapsedSeconds);
                        player.MapSprite.Draw(spriteBatch, new Vector2(PlayerPosition.ScreenPosition.X, PlayerPosition.ScreenPosition.Y), 1);
                    }
                    break;
                case Character.CharacterState.Walking:
                    if (player.WalkingSprite != null)
                    {
                        player.WalkingSprite.UpdateAnimation(elapsedSeconds);
                        player.WalkingSprite.Draw(spriteBatch, new Vector2(PlayerPosition.ScreenPosition.X, PlayerPosition.ScreenPosition.Y),
                            1);
                    }
                    else if (player.MapSprite != null)
                    {
                        player.MapSprite.UpdateAnimation(elapsedSeconds);
                        player.MapSprite.Draw(spriteBatch, new Vector2(PlayerPosition.ScreenPosition.X, PlayerPosition.ScreenPosition.Y),
                            1);
                    }
                    break;
                case Character.CharacterState.Acting:
                    if (Session.IsSelectedItemTool)
                    {
                        Tool tool = (Session.SelectedInventoryItem as Tool);
                        if (tool.Sprite != null)
                        {
                            tool.Direction = PlayerPosition.FacingDirection;
                            tool.ResetAnimation();
                            tool.Sprite.UpdateAnimation(elapsedSeconds);
                            tool.Sprite.Draw(spriteBatch, new Vector2(PlayerPosition.ScreenPosition.X, PlayerPosition.ScreenPosition.Y),
                                1);
                        }
                    }
                    break;
            }
            foreach (Character npc in Session.NPCCharacters)
            {
                Vector2 screenPosVector = new Vector2((float)npc.MapPosition.X, (float)npc.MapPosition.Y);
                screenPosVector = _tileEngine.ToScreenCoords(screenPosVector);
                npc.ResetAnimation(npc.State == Character.CharacterState.Walking);
                switch (npc.State)
                {
                    case Character.CharacterState.Idle:
                        if (npc.MapSprite != null)
                        {
                            npc.MapSprite.UpdateAnimation(elapsedSeconds);
                            npc.MapSprite.Draw(spriteBatch, screenPosVector, 1);
                        }
                        break;
                    case Character.CharacterState.Walking:
                        if (npc.WalkingSprite != null)
                        {
                            npc.WalkingSprite.UpdateAnimation(elapsedSeconds);
                            npc.WalkingSprite.Draw(spriteBatch, screenPosVector,
                                1);
                        }
                        else if (npc.MapSprite != null)
                        {
                            npc.MapSprite.UpdateAnimation(elapsedSeconds);
                            npc.MapSprite.Draw(spriteBatch, screenPosVector,
                                1);
                        }
                        break;
                }
            }
            spriteBatch.End();
            #endregion

            _tileEngine.DrawCrops(spriteBatch, PlayerPosition.MapPosition.Y, _tileEngine.Map.MapHeightInPixels);
            _tileEngine.Draw(spriteBatch, 2);

            spriteBatch.Begin();
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), new Color(1.0f, 1.0f, 1.0f, Clock.DaylightAlpha));

            spriteBatch.Draw(_selectorBox, PlayerPosition.SelectionScreenVector, Color.White);
            //Fonts.DrawCenteredText(spriteBatch, Fonts.DebugFont, String.Format("FPS: {0}", ((int)_fps).ToString()), new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height - 30), Color.Blue);

            _topInventory.Draw(spriteBatch, Clock.ElapsedMilliseconds);
            if (_inventory.Visible)
            {
                _tileEngine.DrawContainers(spriteBatch, _inventory.Position + new Vector2(0, _inventory.Dimensions.Y));
                _inventory.Draw(spriteBatch, Clock.ElapsedMilliseconds);
            }
            _clockAndWeather.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
