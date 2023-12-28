using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.CactusTests;

public class LocationTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void Location_GeneralCall_ReturnsSoilFromMapAtCactusLocation(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var cactus = new Cactus(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Act
		var result = cactus.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(expectedX, expectedY));
	}
}
