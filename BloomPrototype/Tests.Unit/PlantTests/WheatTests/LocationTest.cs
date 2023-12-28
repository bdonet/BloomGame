using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.WheatTests;

public class LocationTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void Location_GeneralCall_ReturnsSoilFromMapAtWheatLocation(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var wheat = new Wheat(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Act
		var result = wheat.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(expectedX, expectedY));
	}
}
