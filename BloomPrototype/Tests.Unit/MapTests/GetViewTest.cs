using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapTests;

public class GetViewTest
{
	[Theory]
	[InlineData(0, 0, 4, 4)]
	[InlineData(2, 2, 4, 4)]
	[InlineData(0, 0, 2, 2)]
	[InlineData(1, 0, 8, 3)]
	public void GetView_GivenCoordinatesMatchesThisMap_ReturnsSoilGridToHoldAllGivenCoordinates(int startX,
																								int startY,
																								int endX,
																								int endY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);
		var startCoordinate = new MapCoordinate(startX, startY, map);
		var endCoordinate = new MapCoordinate(endX, endY, map);

		var expectedXSize = (endX - startX) + 1;
		var expectedYSize = (endY - startY) + 1;

		/// Act
		var result = map.GetView(startCoordinate, endCoordinate);

		/// Assert
		(result.GetUpperBound(0) + 1).ShouldBe(expectedXSize);
		(result.GetUpperBound(1) + 1).ShouldBe(expectedYSize);
	}

	[Fact]
	public void GetView_GivenCoordinatesMatchesThisMap_ReturnsSoilGridOfWithSoilFromMap()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);
		var startCoordinate = new MapCoordinate(0, 0, map);
		var endCoordinate = new MapCoordinate(5, 5, map);

		/// Act
		var result = map.GetView(startCoordinate, endCoordinate);

		/// Assert
		result[0, 0].ShouldBe(map.GetSoil(startCoordinate));
		result[5, 5].ShouldBe(map.GetSoil(endCoordinate));
	}

	[Theory]
	[InlineData(true, false)]
	[InlineData(false, true)]
	[InlineData(true, true)]
	public void GetView_AtLeastOneGivenCoordinateDoesNotMatchThisMap_ThrowsExceptionWithMessage(bool startIsForDifferentMap,
																								bool endIsForDifferentMap)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);

		MapCoordinate startCoordinate;
		if (startIsForDifferentMap)
			startCoordinate = new MapCoordinate(0, 0, MapHelper.SetupTestMap(10));
		else
			startCoordinate = new MapCoordinate(0, 0, map);

		MapCoordinate endCoordinate;
		if (endIsForDifferentMap)
			endCoordinate = new MapCoordinate(5, 5, MapHelper.SetupTestMap(10));
		else
			endCoordinate = new MapCoordinate(5, 5, map);

		/// Act
		var exception = Record.Exception(() => map.GetView(startCoordinate, endCoordinate));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<InvalidOperationException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("for");
		exception.Message.ShouldContain("different");
		exception.Message.ShouldContain(nameof(Map));
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(2, 2)]
	[InlineData(0, 0)]
	[InlineData(1, 0)]
	public void GetView_GivenCoordinateMatchesThisMap_ReturnsSoilGridOfMapViewSize(int startX, int startY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);
		var startCoordinate = new MapCoordinate(startX, startY, map);

		/// Act
		var result = map.GetView(startCoordinate);

		/// Assert
		(result.GetUpperBound(0) + 1).ShouldBe(Map.ViewSize);
		(result.GetUpperBound(1) + 1).ShouldBe(Map.ViewSize);
	}

	[Fact]
	public void GetView_GivenCoordinateMatchesThisMap_ReturnsSoilGridOfWithSoilFromMap()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);
		var startCoordinate = new MapCoordinate(0, 0, map);
		var endCoordinate = new MapCoordinate(Map.ViewSize - 1, Map.ViewSize - 1, map);

		/// Act
		var result = map.GetView(startCoordinate);

		/// Assert
		result[0, 0].ShouldBe(map.GetSoil(startCoordinate));
		result[Map.ViewSize - 1, Map.ViewSize - 1].ShouldBe(map.GetSoil(endCoordinate));
	}

	[Fact]
	public void GetView_GivenCoordinateDoesNotMatchThisMap_ThrowsExceptionWithMessage()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(10);
		var startCoordinate = new MapCoordinate(0, 0, MapHelper.SetupTestMap(10));

		/// Act
		var exception = Record.Exception(() => map.GetView(startCoordinate));

		/// Assert
		exception.ShouldNotBeNull();
		exception.ShouldBeOfType<InvalidOperationException>();
		exception.Message.ShouldContain(nameof(MapCoordinate));
		exception.Message.ShouldContain("for");
		exception.Message.ShouldContain("different");
		exception.Message.ShouldContain(nameof(Map));
	}
}
