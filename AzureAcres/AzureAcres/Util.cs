using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AzureAcres
{
    public static class Util
    {
        public static Texture2D CreateRectangle(GraphicsDevice graphics, int width, int height, Color color)
        {
            Texture2D rectangleTexture = new Texture2D(graphics, width, height);
            Color[] colorArray = new Color[width * height];
            for (int i = 0; i < colorArray.Length; i++)
                colorArray[i] = color;
            rectangleTexture.SetData(colorArray);
            return rectangleTexture;
        }
        public static Rectangle CreateRectangle(Vector2 position, Vector2 dimensions)
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)dimensions.X,
                (int)dimensions.Y
                );
        }
        public static Rectangle CreateRectangle(Vector2 position, Texture2D texture)
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height
                );
        }
    }
}
