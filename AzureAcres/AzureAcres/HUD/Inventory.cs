using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using AzureAcresData;

namespace AzureAcres
{
    public class Inventory
    {
        private Texture2D _selectedTexture;
        private Texture2D _texture;
        public Vector2 Dimensions { get { return new Vector2(_texture.Width, _texture.Height); } }
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private string _selectedItemName;
        public int Width { get { return _texture.Width; } }
        private bool _visible = false;
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        private TopInventory _topInventory;
        public TopInventory TopInventory
        {
            get { return _topInventory; }
            set { _topInventory = value; }
        }
        private Vector2 _hoveringItem;
        private Vector2 _selectedItem;

        public Inventory(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>("Textures/HUD/Inventory");
            _selectedTexture = Content.Load<Texture2D>("Textures/HUD/SelectedItem");
            _hoveringItem = new Vector2(0, 0);
            _selectedItem = new Vector2(-2, -2);
        }

        public void OpenInventory()
        {
            Visible = true;
            Session.SelectedInventoryItem = null;
            _topInventory.SelectedIndex = -2;
            _hoveringItem = new Vector2(0, 0);
        }

        public void HandleInput()
        {
            if (InputManager.IsActionTriggered(InputManager.Action.MainMenu))
            {
                Visible = false;
                return;
            }

            /// Input for moving around the menu
            if (InputManager.IsActionTriggered(InputManager.Action.MoveCharacterUp)) _hoveringItem += new Vector2(0, -1);
            else if (InputManager.IsActionTriggered(InputManager.Action.MoveCharacterDown)) _hoveringItem += new Vector2(0, 1);
            else if (InputManager.IsActionTriggered(InputManager.Action.MoveCharacterLeft)) _hoveringItem += new Vector2(-1, 0);
            else if (InputManager.IsActionTriggered(InputManager.Action.MoveCharacterRight)) _hoveringItem += new Vector2(1, 0);

            if (_hoveringItem.X > 3)
            {
                if (_hoveringItem.Y != -1) _hoveringItem.X = 0;
                if (_hoveringItem.X > 4)
                    _hoveringItem.X = 0;
            }
            if (_hoveringItem.X < 0)
            {
                if (_hoveringItem.Y != -1) _hoveringItem.X = 3;
                else _hoveringItem.X = 4;
            }
            if (_hoveringItem.Y > 3) _hoveringItem.Y = -1;
            if (_hoveringItem.Y < -1) _hoveringItem.Y = 3;

            if (InputManager.IsActionTriggered(InputManager.Action.Ok))
            {
                /// if there is no item selected
                if (_selectedItem == new Vector2(-2, -2))
                {
                    if (_hoveringItem.Y == -1)
                        _topInventory.SelectedIndex = (int)_hoveringItem.X;
                    _selectedItem = _hoveringItem; /// set equal to the hovering 
                }
                else
                {
                    if (_selectedItem == _hoveringItem)
                        _selectedItem = new Vector2(-2, -2);
                    else
                    {

                        if (_selectedItem.Y == -1 ||
                            _hoveringItem.Y == -1)
                        {
                            if (_selectedItem.Y == -1 && _hoveringItem.Y == -1)
                                Session.SwapTopInventory((int)_hoveringItem.X, (int)_selectedItem.X);
                            else if (_selectedItem.Y == -1)
                                Session.SwapMainAndTopInventory((int)(_hoveringItem.X + _hoveringItem.Y * 4), (int)_selectedItem.X);
                            else if (_hoveringItem.Y == -1)
                                Session.SwapMainAndTopInventory((int)(_selectedItem.X + _selectedItem.Y * 4), (int)_hoveringItem.X);
                        }
                        else
                        {
                            /// Switch the items
                            int firstIndex = (int)(_selectedItem.X + _selectedItem.Y * 4);
                            int secondIndex = (int)(_hoveringItem.X + _hoveringItem.Y * 4);
                            Session.SwapMainInventory(firstIndex, secondIndex);
                        }
                        _selectedItem = new Vector2(-2, -2);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, float elapsedMilliseconds)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            Vector2 drawpos = new Vector2(Position.X, Position.Y + 35);
            if (_hoveringItem != new Vector2(-2, -2))
            {
                _topInventory.SelectedIndex = -1;
                if (_hoveringItem.Y != -1)
                    spriteBatch.Draw(_selectedTexture, Position + new Vector2(0, 34) + _hoveringItem * new Vector2(32, 32), Color.White);
                else
                    _topInventory.SelectedIndex = (int)_hoveringItem.X;
            }
            if (_selectedItem != new Vector2(-2, -2) &&
                _selectedItem.Y != -1)
                spriteBatch.Draw(_selectedTexture, Position + new Vector2(0, 34) + _selectedItem * new Vector2(32, 32), Color.White);

            for (int i = 0; i < Session.Inventory.Length; ++i)
            {
                ICollectableItem item = Session.Inventory[i];
                if (item != null)
                {
                    Texture2D tex = item.GetItemTexture();
                    if (tex != null)
                        spriteBatch.Draw(item.GetItemTexture(), drawpos, Color.White);
                }
                drawpos += new Vector2(32, 0);
                if ((i + 1) % 4 == 0)
                    drawpos += new Vector2(-128, 32);
            }
            if (_selectedItem.Y == -1)
                _topInventory.SelectedIndex = (int)_selectedItem.X;
        }
    }
}
