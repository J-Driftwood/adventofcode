namespace Sea_Cucumber.Models
{
    internal class Coordinates
    {
        internal Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
