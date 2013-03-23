using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AzureAcresData
{
    public class AnimatedTile : ActionableObject, ICloneable
    {
        private AnimatingSprite _sprite;
        public AnimatingSprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        private Vector2 _coordinates;
        [ContentSerializerIgnore]
        public Vector2 Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public class AnimatedTileReader : ContentTypeReader<AnimatedTile>
        {
            protected override AnimatedTile Read(ContentReader input, AnimatedTile existingInstance)
            {
                AnimatedTile existing = existingInstance;
                if (existing == null)
                    existing = new AnimatedTile();
                existing.Name = input.ReadString();
                existing.Sprite = input.ReadObject<AnimatingSprite>();
                if (existing.Sprite != null)
                {
                    existing.Sprite.SourceOffset =
                        new Vector2(
                        existing.Sprite.SourceOffset.X - 32,
                        existing.Sprite.SourceOffset.Y - 32);
                }
                return existing;
            }
        }
        public object Clone()
        {
            AnimatedTile o = new AnimatedTile();
            o.Coordinates = this.Coordinates;
            o.Sprite = this.Sprite;
            return o;
        }
    }
}
