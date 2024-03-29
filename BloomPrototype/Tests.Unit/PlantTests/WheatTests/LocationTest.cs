﻿using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
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
		var coordinate = new MapCoordinate(expectedX, expectedY, map);

		var wheat = new Wheat(map,
							expectedX,
							expectedY,
							PlantMaturity.Sprout,
							PlantHealth.Stable,
							0,
							Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = wheat.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(coordinate));
	}
}
