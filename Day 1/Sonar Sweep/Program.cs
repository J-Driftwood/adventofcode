using Sonar_Sweep.Models;
using Sonar_Sweep.Services;
using System;

namespace Sonar_Sweep
{
    class Program
    {
        static void Main(string[] args)
        {
            var depthMeassurements = DepthMeassurements.FromResource("Sonar_Sweep.Resources.DepthMeasurements.txt");

            // Part 1
            var result = DepthMeassurementService.CountDepthMeassurementIncreases(depthMeassurements);

            Console.WriteLine($"Part 1: There are {result} entries which were bigger than the previous value.");

            // Part 2
            var slidingWindowList = DepthMeassurementService.ConvertToSlidingWindowList(depthMeassurements);
            result = DepthMeassurementService.CountDepthMeassurementIncreases(slidingWindowList);

            Console.WriteLine($"Part 2: There are {result} entries which were bigger than the previous value.");
        }
    }
}
