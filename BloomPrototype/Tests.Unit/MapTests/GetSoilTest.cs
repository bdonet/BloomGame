using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapTests;

public class GetSoilTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void GetSoil_GivenCoordinateIsForThisMap_ReturnsSoilAtGivenCoordinates(int x, int y)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var coordinate = new MapCoordinate(x, y, map);

		/// Act
		var result = map.GetSoil(coordinate);

		/// Assert
		result.ShouldBe(map.Grid[x, y]);
	}

	[Fact]
	public void GetSoil_GivenCoordinateIsForDifferentMap_ThrowsExceptionWithMessage()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var coordinate = new MapCoordinate(1, 1, MapHelper.SetupTestMap(2));

		/// Act
		var exception = Record.Exception(() => map.GetSoil(coordinate));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<InvalidOperationException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("for");
		exception.Message.ShouldContain("different");
		exception.Message.ShouldContain(nameof(Map));
	}
}
