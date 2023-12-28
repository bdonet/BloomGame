using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.TomatoTests;

public class LocationTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void Location_GeneralCall_ReturnsSoilFromMapAtTomatoLocation(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var tomato = new Tomato(map, expectedX, expectedY);

		/// Act
		var result = tomato.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(expectedX, expectedY));
	}
}
