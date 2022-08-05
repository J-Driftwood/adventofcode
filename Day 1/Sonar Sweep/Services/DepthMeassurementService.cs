using Sonar_Sweep.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sonar_Sweep.Services
{
    internal class DepthMeassurementService
    {
        /// <returns>Number of entires that were bigger than the preceding entry.</returns>
        public static int CountDepthMeassurementIncreases(DepthMeassurements depthMeassurements)
        {
            var result = 0;
            var precedingDepthMeassurement = depthMeassurements.Values.FirstOrDefault();

            foreach (var depthMeassurement in depthMeassurements.Values.Skip(1))
            {
                if (DepthHasIncreased(depthMeassurement, precedingDepthMeassurement))
                {
                    result++;
                }

                precedingDepthMeassurement = depthMeassurement;
            }

            return result;
        }

        private static bool DepthHasIncreased(int currentDepth, int precedingDepth) => currentDepth > precedingDepth;

        internal static DepthMeassurements ConvertToSlidingWindowList(DepthMeassurements depthMeassurements)
        {
            var values = new List<int>();

            for (int i = 0; i < depthMeassurements.Values.Count - 2; i++)
            {
                values.Add(GetWindowValue(depthMeassurements.Values, i));
            }

            return new DepthMeassurements(values);
        }

        private static int GetWindowValue(IReadOnlyList<int> values, int currentIndex, int windowSize = 3)
        {
            var result = 0;

            for (int i = 0; i < windowSize; i++)
            {
                result += values[currentIndex + i];
            }

            return result;
        }
    }
}
