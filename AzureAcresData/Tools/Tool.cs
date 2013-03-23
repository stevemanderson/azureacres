using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AzureAcresData
{
    public class Tool : ContentObject, ICloneable, ICollectableItem
    {
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _inventoryTextureName;
        public string InventoryTextureName
        {
            get { return _inventoryTextureName; }
            set { _inventoryTextureName = value; }
        }
        private Texture2D _inventoryTexture;
        [ContentSerializerIgnore]
        public Texture2D InventoryTexture
        {
            get { return _inventoryTexture; }
            set { _inventoryTexture = value; }
        }

        private AnimatingSprite _sprite;
        public AnimatingSprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
        private Direction _direction;
        [ContentSerializerIgnore]
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        [ContentSerializerIgnore]
        public bool IsPlaybackComplete
        {
            get { if (_sprite != null) return _sprite.IsPlaybackComplete; return false; }
        }
        
        public virtual void ResetAnimation()
        {
            if (_sprite != null)
            {
                _sprite.PlayAnimation(Type.ToString(), Direction);
                if (_sprite.IsPlaybackComplete)
                    _sprite.CurrentAnimation = null;
            }
        }

        public class ToolReader : ContentTypeReader<Tool>
        {
            protected override Tool Read(ContentReader input, Tool existingInstance)
            {
                Tool existing = existingInstance;
                if (existing == null)
                    existing = new Tool();
                existing.Name = input.ReadString();
                existing.Type = input.ReadString();
                existing.InventoryTextureName = input.ReadString();
                existing.InventoryTexture = input.ContentManager.Load<Texture2D>(existing.InventoryTextureName);
                existing.Sprite = input.ReadObject<AnimatingSprite>();
                if (existing.Sprite != null)
                {
                    existing.Sprite.SourceOffset =
                        new Vector2(
                        existing.Sprite.SourceOffset.X - 32,
                        existing.Sprite.SourceOffset.Y - 32);
                }
                existing.ResetAnimation();
                return existing;
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D GetItemTexture()
        {
            return InventoryTexture;
        }

        public string GetItemName()
        {
            return this.Name;
        }
    }
}
