using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
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
		var coordinate = new MapCoordinate(expectedX, expectedY, map);

		var tomato = new Tomato(map,
								expectedX,
								expectedY,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = tomato.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(coordinate));
	}
}
