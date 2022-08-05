using FluentAssertions;
using Sonar_Sweep.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace Sonar_Sweep.Tests
{
    public class DepthMeassurementServiceTests
    {
        private readonly Assembly _assembly;
        public DepthMeassurementServiceTests()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        [Fact]
        public void FromResource_WithoutFile_ThrowsFileNotFoundException()
        {
            // Arrange
            var nonExistingFileLocation = "FileNotHere.txt";
            var expectedExceptionMessage = $"No file found at location {nonExistingFileLocation}";

            // Act
            Action action = () => DepthMeassurements.FromResource(_assembly, nonExistingFileLocation);

            // Assert
            action.Should().Throw<FileNotFoundException>().WithMessage(expectedExceptionMessage);
        }

        [Fact]
        public void FromResource_WithFileContainingText_ThrowsInvalidFormatException()
        {
            // Arrange
            var fileLocation = "Sonar_Sweep.Tests.Resources.DepthMeasurementsContainingText.txt";

            // Act
            Action action = () => DepthMeassurements.FromResource(_assembly, fileLocation);

            // Assert
            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void FromResource_WithEmptyFile_ReturnsEmptyList()
        {
            // Arrange
            var fileLocation = "Sonar_Sweep.Tests.Resources.EmptyDepthMeasurements.txt";

            // Act
            var result = DepthMeassurements.FromResource(_assembly, fileLocation);

            // Assert
            result.Values.Should().BeEmpty();
        }

        [Fact]
        public void FromResource_WithValidFile_ReturnsValidObject()
        {
            // Arrange
            var fileLocation = "Sonar_Sweep.Tests.Resources.ValidDepthMeasurements.txt";
            var expectedResult = new List<int>
            {
                1,
                4,
                5,
                2,
                6,
            };

            // Act
            var result = DepthMeassurements.FromResource(_assembly, fileLocation);

            // Assert
            result.Values.Should().Equal(expectedResult);
        }
    }
}
