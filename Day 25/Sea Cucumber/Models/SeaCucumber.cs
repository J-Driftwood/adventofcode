using System;
using static Sea_Cucumber.Enums.Enums;

namespace Sea_Cucumber.Models
{
    /// <summary>
    /// Represents the enitity on the #SeaMap's grid
    /// </summary>
    internal class SeaCucumber
    {
        internal SeaCucumber(int x, int y, CardinalDirection direction)
        {
            Coordinates = new Coordinates(x, y);
            Direction = direction;
        }

        internal Coordinates Coordinates { get; private set; }
        internal CardinalDirection Direction { get; private set; }
        internal bool CanMove { get; set; } = false;

        internal Coordinates GetTargetPosition(int gridWidth, int gridHeight)
        {
            var wrappedYNodeIndex = Coordinates.Y + 1 < gridHeight ? Coordinates.Y + 1 : 0;
            var wrappedXNodeIndex = Coordinates.X + 1 < gridWidth ? Coordinates.X + 1 : 0;

            switch (Direction)
            {
                case CardinalDirection.South:
                    return new Coordinates(Coordinates.X, wrappedYNodeIndex);
                case CardinalDirection.East:
                    return new Coordinates(wrappedXNodeIndex, Coordinates.Y);
                default:
                    throw new NotImplementedException();
            }
        }

        internal void Move(SeaMap seaMap)
        {
            seaMap.Map[Coordinates.Y][Coordinates.X] = '.';

            var targetPos = GetTargetPosition(seaMap.Width, seaMap.Height);
            Coordinates.X = targetPos.X;
            Coordinates.Y = targetPos.Y;

            seaMap.Map[Coordinates.Y][Coordinates.X] = GetMapCharacter();
        }

        private char GetMapCharacter()
        {
            switch (Direction)
            {
                case CardinalDirection.South:
                    return 'v';
                case CardinalDirection.East:
                    return '>';
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
