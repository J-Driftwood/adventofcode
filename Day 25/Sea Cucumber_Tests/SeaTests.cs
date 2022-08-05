using FluentAssertions;
using Sea_Cucumber.Models;
using Xunit;

namespace Sea_Cucumber_Tests
{
    public class SeaTests
    {
        [Fact]
        public void Iterate_WithSingleLineInputAnd2Iterations_MovesWhenAllowed()
        {
            // Arrange
            var input = "...>>>>>...";
            var expected = "...>>>.>.>.";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            sea.Iterate();
            sea.Iterate();

            // Assert
            var result = sea.SeaMap.AsString();
            result.Should().Be(expected);
        }

        [Fact]
        public void Iterate_WithCucumbersMovingEast_AllowsMovementWhenNotBlocked()
        {
            // Arrange
            var input =
                ".v....>" + "\r\n" + //wraps around boundry
                ".v>...." + "\r\n" + //can move freely
                ".>>...." + "\r\n" + //blocked by other entity
                "v.....>";           //blocked by other entity

            var expected =
                ">v....." + "\r\n" +
                ".v.>..." + "\r\n" +
                ".>.>..." + "\r\n" +
                "v.....>";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            sea.Iterate();

            // Assert
            var result = sea.SeaMap.AsString();
            result.Should().Be(expected);
        }

        [Fact]
        public void Iterate_WithCucumbersMovingSouth_AllowsMovementWhenNotBlocked()
        {
            // Arrange
            var input =
                "v.v.v.." + "\r\n" +
                "..v...v" + "\r\n" +
                "......." + "\r\n" +
                "....v.v";

            var expected =
                "..v...v" + "\r\n" +
                "v...v.." + "\r\n" +
                "..v...v" + "\r\n" +
                "....v..";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            sea.Iterate();

            // Assert
            var result = sea.SeaMap.AsString();
            result.Should().Be(expected);
        }

        [Fact]
        public void Iterate_WithCucumbersMovingMultipleDirections_AllowsMovementWhenNotBlocked()
        {
            // Arrange
            var input =
                "...>..." + "\r\n" +
                "......." + "\r\n" +
                "......>" + "\r\n" +
                "v.....>" + "\r\n" +
                "......>" + "\r\n" +
                "......." + "\r\n" +
                "..vvv..";

            var expected =
                ">......" + "\r\n" +
                "..v...." + "\r\n" +
                "..>.v.." + "\r\n" +
                ".>.v..." + "\r\n" +
                "...>..." + "\r\n" +
                "......." + "\r\n" +
                "v......";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            for (int i = 0; i < 4; i++)
            {
                sea.Iterate();
            }

            // Assert
            var result = sea.SeaMap.AsString();
            result.Should().Be(expected);
        }

        [Fact]
        public void Iterate_WithAllSeaCucumbersStuck_SetsAnyMovementToFalse()
        {
            // Arrange
            var input =
                "..>>v>vv.." + "\r\n" +
                "..v.>>vv.." + "\r\n" +
                "..>>v>>vv." + "\r\n" +
                "..>>>>>vv." + "\r\n" +
                "v......>vv" + "\r\n" +
                "v>v....>>v" + "\r\n" +
                "vvv.....>>" + "\r\n" +
                ">vv......>" + "\r\n" +
                ".>v.vv.v..";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            sea.Iterate();

            // Assert
            sea.SeaMap.AsString().Should().Be(input);
            sea.AnyMovement.Should().BeFalse();
        }

        [Fact]
        public void GetMaxIterations_WithCucumbersMovingMultipleDirections_AllowsMovementWhenNotBlocked()
        {
            // Arrange
            var input =
                "v...>>.vv>" + "\r\n" +
                ".vv>>.vv.." + "\r\n" +
                ">>.>v>...v" + "\r\n" +
                ">>v>>.>.v." + "\r\n" +
                "v>v.vv.v.." + "\r\n" +
                ">.>>..v..." + "\r\n" +
                ".vv..>.>v." + "\r\n" +
                "v.v..>>v.v" + "\r\n" +
                "....v..v.>";

            var expectedMap =
                "..>>v>vv.." + "\r\n" +
                "..v.>>vv.." + "\r\n" +
                "..>>v>>vv." + "\r\n" +
                "..>>>>>vv." + "\r\n" +
                "v......>vv" + "\r\n" +
                "v>v....>>v" + "\r\n" +
                "vvv.....>>" + "\r\n" +
                ">vv......>" + "\r\n" +
                ".>v.vv.v..";


            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            var result = sea.GetMaxIterations();

            // Assert
            var newSeaMap = sea.SeaMap.AsString();
            newSeaMap.Should().Be(expectedMap);
            result.Should().Be(58);
        }

        [Fact]
        public void GetMaxIterations_WithNeverEndingMap_ThrowsExceptionWithMessage()
        {
            // Arrange
            var input = "...>...";

            var seaMap = SeaMap.FromString(input);
            var sea = new Sea(seaMap);

            // Act
            Action action = () => sea.GetMaxIterations();

            // Assert
            action.Should().Throw<Exception>().WithMessage("Movement not stuck after more than 1000 iterations.");
        }
    }
}