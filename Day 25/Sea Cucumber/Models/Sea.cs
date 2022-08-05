using System.Collections.Generic;
using System.Linq;
using static Sea_Cucumber.Enums.Enums;

namespace Sea_Cucumber.Models
{
    internal class Sea
    {
        internal Sea(SeaMap seaMap)
        {
            SeaMap = seaMap;

            for (var y = 0; y < SeaMap.Map.Count; y++)
            {
                for (var x = 0; x < SeaMap.Map[y].Count; x++)
                {
                    switch (SeaMap.Map[y][x])
                    {
                        case '>':
                            SeaCucumbers.Add(new SeaCucumber(x, y, CardinalDirection.East));
                            break;
                        case 'v':
                            SeaCucumbers.Add(new SeaCucumber(x, y, CardinalDirection.South));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal SeaMap SeaMap { get; set; }
        internal List<SeaCucumber> SeaCucumbers { get; set; } = new List<SeaCucumber>();
        internal int IterationCounter { get; set; } = 0;
        internal bool AnyMovement { get; set; } = false;

        internal static readonly List<CardinalDirection> CardinalDirectionOrder = new List<CardinalDirection>
        {
            CardinalDirection.East,
            CardinalDirection.South,
        };

        internal void Iterate()
        {
            AnyMovement = false;

            foreach (var direction in CardinalDirectionOrder)
            {
                var seaCucumbers = SeaCucumbers.Where(x => x.Direction == direction);

                foreach (var seaCucumber in seaCucumbers)
                {
                    var targetPos = seaCucumber.GetTargetPosition(SeaMap.Width, SeaMap.Height);
                    seaCucumber.CanMove = PositionIsAvailable(targetPos);
                }

                foreach (var seaCucumber in seaCucumbers.Where(x => x.CanMove))
                {
                    seaCucumber.Move(SeaMap);
                    AnyMovement = true;
                }
            }

            IterationCounter++;
        }

        private bool PositionIsAvailable(Coordinates targetPos)
        {
            return SeaMap.Map[targetPos.Y][targetPos.X] == '.';
        }

        internal int GetMaxIterations()
        {
            var infinityBreakCounter = 0;

            do
            {
                Iterate();

                infinityBreakCounter++;
                if (infinityBreakCounter >= 1000)
                {
                    throw new System.Exception("Movement not stuck after more than 1000 iterations.");
                }
            } while (AnyMovement);

            return IterationCounter;
        }
    }
}
