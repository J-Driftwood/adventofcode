using Sea_Cucumber.Models;
using Sea_Cucumber.Services;
using System;

namespace Sea_Cucumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ResourceLoader.GetDataFromResource("Sea_Cucumber.Resources.SeaCucumberPositions.txt");

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            var maxIterations = sea.GetMaxIterations();

            Console.WriteLine($"After {maxIterations} iterations, the submarine should have the requied space.");
        }
    }
}
