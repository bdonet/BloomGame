using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapCoordinateTests;

public class ctorTests
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(1, 2)]
	[InlineData(10, 15)]
	[InlineData(19, 19)]
	public void ctor_GivenCoordinatesAreValid_CreatedObjectHasGivenCoordinates(int expectedX, int expectedY)
	{
		/// Arrange

		/// Act
		var result = new MapCoordinate(expectedX, expectedY, MapHelper.SetupTestMap(20));

		/// Assert
		result.X.ShouldBe(expectedX);
		result.Y.ShouldBe(expectedY);
	}

	[Theory]
	[InlineData(-4, 16)]
	[InlineData(8, -21)]
	[InlineData(-2, -8)]
	public void ctor_AtLeastOneGivenCoordinateIsNegative_ThrowsExceptionWithMessage(int x, int y)
	{
		/// Arrange

		/// Act
		var exception = Record.Exception(() => new MapCoordinate(x, y, MapHelper.SetupTestMap(1)));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<ArgumentException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("negative");
		exception.Message.ShouldContain("coordinate");
	}

	[Theory]
	[InlineData(1, 0)]
	[InlineData(0, 1)]
	[InlineData(2, 3)]
	[InlineData(int.MaxValue, int.MaxValue)]
	public void ctor_AtLeastOneCoordinateIsBeyondEdgeOfGivenMap_ThrowsExceptionWithMessage(int x, int y)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var exception = Record.Exception(() => new MapCoordinate(x, y, map));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<ArgumentException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("coordinate");
		exception.Message.ShouldContain("beyond");
		exception.Message.ShouldContain("edge");
		exception.Message.ShouldContain("map");
	}
}
