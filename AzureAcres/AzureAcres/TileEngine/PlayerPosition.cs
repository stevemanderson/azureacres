using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AzureAcresData;

namespace AzureAcres
{
    public static class PlayerPosition
    {
        private static Vector2 SelectionVector(Point orig)
        {
            Vector2 result = new Vector2(0, 0);
            switch (FacingDirection)
            {
                case Direction.North:
                    result.X = orig.X + Width / 2;
                    result.Y = orig.Y - (int)(Height * 0.75);
                    break;
                case Direction.South:
                    result.X = orig.X + Width / 2;
                    result.Y = (orig.Y + Height) + (int)(Height * 0.75);
                    break;
                case Direction.West:
                    result.X = orig.X - (int)(Width * 0.75);
                    result.Y = orig.Y + Height / 2;
                    break;
                case Direction.East:
                    result.X = Width + (orig.X + (int)(Width * 0.75));
                    result.Y = orig.Y + Height / 2;
                    break;
            }
            return result;
        }

        public static Point ScreenPosition = new Point(0, 0);
        public static Point MapPosition = new Point(0, 0);
        public static int Width = 0;
        public static int Height = 0;
        public static Rectangle BoundingBox { get { return new Rectangle(MapPosition.X + 5, MapPosition.Y + 5, Width - 10, Height - 10); } }
        public static Point PreviousScreenPosition = new Point(0, 0);
        public static Point PreviousMapPosition = new Point(0, 0);
        public static Vector2 SelectionMapVector { get { return SelectionVector(MapPosition); } }
        public static Vector2 SelectionScreenVector { get { return SelectionVector(ScreenPosition); } }
        public static Direction FacingDirection = Direction.South;
        public static void SetMapPosition(Point p)
        {
            PreviousMapPosition = MapPosition;
            MapPosition = p;
        }
        public static void SetScreenPosition(Point p)
        {
            PreviousScreenPosition = ScreenPosition;
            ScreenPosition = p;
        }
        public static void Move(Vector2 move)
        {
            PreviousMapPosition = MapPosition;
            PreviousScreenPosition = ScreenPosition;
            if (move != Vector2.Zero)
            {
                IsMoving = true;
                MapPosition.X += (int)move.X;
                MapPosition.Y += (int)move.Y;
                if (move.Y < 0) FacingDirection = Direction.North;
                if (move.Y > 0) FacingDirection = Direction.South;
                if (move.X < 0) FacingDirection = Direction.West;
                if (move.X > 0) FacingDirection = Direction.East;
            }
            else
                IsMoving = false;
        }
        public static bool IsMoving { get; set; }
        public static Point CenterBoundingBoxPosition
        {
            get
            {
                return new Point(BoundingBox.Width / 2 + BoundingBox.X, BoundingBox.Height / 2 + BoundingBox.Y);
            }
        }
    }
}
