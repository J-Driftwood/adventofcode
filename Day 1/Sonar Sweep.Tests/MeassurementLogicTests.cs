using FluentAssertions;
using Sonar_Sweep.Models;
using Sonar_Sweep.Services;
using System.Collections.Generic;
using Xunit;

namespace Sonar_Sweep.Tests
{
    public class MeassurementLogicTests
    {
        [Fact]
        public void MeassureDepthChanges_WithDepthIncreases_ReturnsExpectdAmountofDepthIncreases()
        {
            // Arrange
            var depthEntries = new List<int>
            {
                199,
                200, // (increased)
                208, // (increased)
                210, // (increased)
                200, // (decreased)
                207, // (increased)
                240, // (increased)
                269, // (increased)
                260, // (decreased)
                263, // (increased)
            };
            var depthMeassurements = new DepthMeassurements(depthEntries);

            // Act
            var result = DepthMeassurementService.CountDepthMeassurementIncreases(depthMeassurements);

            // Assert
            result.Should().Be(7);
        }

        [Fact]
        public void MeassureDepthChanges_WithNoIncreases_ReturnsZero()
        {
            // Arrange
            var depthEntries = new List<int>
            {
                263, // (decreased)
                262, // (decreased)
                261, // (decreased)
            };
            var depthMeassurements = new DepthMeassurements(depthEntries);

            // Act
            var result = DepthMeassurementService.CountDepthMeassurementIncreases(depthMeassurements);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void MeassureDepthChanges_WithNoValue_ReturnsZero()
        {
            // Arrange
            var depthEntries = new List<int>();
            var depthMeassurements = new DepthMeassurements(depthEntries);

            // Act
            var result = DepthMeassurementService.CountDepthMeassurementIncreases(depthMeassurements);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void ConvertToSlidingWindowList_WithValues_ReturnsValidSlidingWindowObject()
        {
            // Arrange
            var depthEntries = new List<int>
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263,
            };

            var expectedDepthEntries = new List<int>
            {
                607,
                618,
                618,
                617,
                647,
                716,
                769,
                792,
            };

            var depthMeassurements = new DepthMeassurements(depthEntries);

            // Act
            var result = DepthMeassurementService.ConvertToSlidingWindowList(depthMeassurements);

            // Assert
            result.Values.Should().Equal(expectedDepthEntries);
        }
    }
}
