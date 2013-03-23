using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AzureAcres
{
    public class ClockAndWeather
    {
        private Texture2D _overlay;
        private Texture2D _seasonsTexture;
        private int _seasonImageOffset = 2;

        public enum WEATHER { SUNNY, RAINING, SNOWING }

        public WEATHER CurrentWeather { set; get; }

        private Texture2D _boxTexture;
        
        private Vector2 _position = Vector2.Zero;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Width
        {
            get { return _boxTexture.Width; }
        }

        public ClockAndWeather(GraphicsDevice graphics, ContentManager Content)
        {
            _seasonsTexture = Content.Load<Texture2D>("Textures/HUD/Weather");
            _boxTexture = Content.Load<Texture2D>("Textures/HUD/ClockAndWeatherBackground");
            CurrentWeather = WEATHER.SUNNY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_boxTexture, Position, Color.White);
            Rectangle srcRect = new Rectangle(((int)CurrentWeather)*32, 0, 32, 32);
            srcRect.X += _seasonImageOffset;
            srcRect.Y += _seasonImageOffset;
            srcRect.Width -= _seasonImageOffset * 2;
            srcRect.Height -= _seasonImageOffset * 2;
            spriteBatch.Draw(_seasonsTexture, new Rectangle((int)Position.X, (int)Position.Y, 28, 28), srcRect, Color.White);
            Fonts.DrawText(spriteBatch, Fonts.DebugFont, String.Format("{0}:{1}", Clock.Hours.ToString("00"), Clock.Minutes.ToString("00")), Position + new Vector2(150, 5), Color.Black);
            Fonts.DrawText(spriteBatch, Fonts.DebugFont, String.Format("{0} {1}", Clock.MonthString, Clock.Day.ToString("00")), Position + new Vector2(140, 23), Color.Black);
            Fonts.DrawText(spriteBatch, Fonts.DebugFont, String.Format("{0}", Clock.GameDateTime.DayOfWeek), Position + new Vector2(40, 23), Color.Black);
            Fonts.DrawText(spriteBatch, Fonts.DebugFont, String.Format("{0}", Clock.Season), Position + new Vector2(40, 5), Color.Black);
        }
    }
}
