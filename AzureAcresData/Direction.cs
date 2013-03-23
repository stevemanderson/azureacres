using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AzureAcresData
{
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    public static class DirectionHelper
    {
        public static Direction DirectionFromString(string direction)
        {
            switch (direction.ToUpper())
            {
                case "NORTH":
                    return Direction.North;
                case "SOUTH":
                    return Direction.South;
                case "EAST":
                    return Direction.East;
                case "WEST":
                    return Direction.West;
                default:
                    return Direction.North;
            }
        }

        public static Vector2 DirectionVectorFromString(string direction)
        {
            switch (DirectionFromString(direction))
            {
                case Direction.South:
                    return new Vector2(0, 1);
                case Direction.West:
                    return new Vector2(-1, 0);
                case Direction.East:
                    return new Vector2(1, 0);
                case Direction.North:
                default:
                    return new Vector2(0, -1);
            }
        }
    }
}
