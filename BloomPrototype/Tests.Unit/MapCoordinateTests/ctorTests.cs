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
	[InlineData(16, 81)]
	[InlineData(int.MaxValue, int.MaxValue)]
	public void ctor_GivenCoordinatesAreValid_CreatedObjectHasGivenCoordinates(int expectedX, int expectedY)
	{
		/// Arrange

		/// Act
		var result = new MapCoordinate(expectedX, expectedY);

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
		var exception = Record.Exception(() => new MapCoordinate(x, y));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<ArgumentException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("negative");
		exception.Message.ShouldContain("coordinate");
	}
}
