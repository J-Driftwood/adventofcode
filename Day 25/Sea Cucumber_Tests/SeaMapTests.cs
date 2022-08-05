using FluentAssertions;
using Sea_Cucumber.Models;
using Xunit;

namespace Sea_Cucumber_Tests
{
    public class SeaMapTests
    {
        [Fact]
        public void FromString_WithInconsistantRowLengths_ThrowsException()
        {
            // Arrange
            var input = "..........\r\n" +
                        ".>v....v\r\n" +
                        ".......>\r\n" +
                        "..........";
            // Act
            Action action = () => SeaMap.FromString(input);

            // Assert
            action.Should().Throw<Exception>().WithMessage("Input is not a perfect rectangle.");
        }

        [Fact]
        public void FromString_WithConsistantRowLengths_ReturnsValidObject()
        {
            // Arrange
            var input = "..........\r\n" +
                        ".>v....v..\r\n" +
                        ".......>..\r\n" +
                        "..........";
            // Act
            var result = SeaMap.FromString(input);

            // Assert
            result.Map.Should().NotBeNull();
            result.Map[0][0].Should().Be('.');
            result.Map[1][1].Should().Be('>');
            result.Map[1][2].Should().Be('v');
            result.Map[1][7].Should().Be('v');
            result.Map[2][7].Should().Be('>');
        }

        [Fact]
        public void FromString_WithSingularRow_ReturnsValidObject()
        {
            // Arrange
            var input = ".>v.......";

            // Act
            var result = SeaMap.FromString(input);

            // Assert
            result.Map.Should().NotBeNull();
            result.Map[0][0].Should().Be('.');
            result.Map[0][1].Should().Be('>');
            result.Map[0][2].Should().Be('v');
        }

        [Fact]
        public void AsString_WithValidMultilineInput_ReturnsIdentialOutput()
        {
            // Arrange
            var input = ".>.v.\r\n" +
                        ".>.v.";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            var result = sea.SeaMap.AsString();

            // Assert
            result.Should().Be(input);
        }

        [Fact]
        public void AsString_WithValidSinglelineInput_ReturnsIdentialOutput()
        {
            // Arrange
            var input = ".>.v.";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            var result = sea.SeaMap.AsString();

            // Assert
            result.Should().Be(input);
        }

        [Fact]
        public void FromString_WithInvalidCharacter_ThrowsFormatException()
        {
            // Arrange
            var input = ".!.";

            // Act
            Action action = () => SeaMap.FromString(input);

            // Assert
            action.Should().Throw<FormatException>().WithMessage("Map contains invalid character");
        }

        [Fact]
        public void FromString_WithNoCharacters_ReturnsValidObject()
        {
            // Arrange
            var input = "";

            // Act
            var result = SeaMap.FromString(input);

            // Assert
            result.Map.Should().BeEmpty();
        }
    }
}