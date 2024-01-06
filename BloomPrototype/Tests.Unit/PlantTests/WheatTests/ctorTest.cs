﻿using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.WheatTests;

public class ctorTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void ctor_GeneralCall_SetsWheatLocationToGivenLocationXAndLocationY(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		var coordinate = new MapCoordinate(expectedX, expectedY, map);

		/// Act
		var wheat = new Wheat(map, expectedX, expectedY, PlantMaturity.Sprout, PlantHealth.Stable);

		/// Assert
		wheat.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var wheat = new Wheat(map, 0, 0, PlantMaturity.Sprout, PlantHealth.Stable);

		/// Assert
		wheat.Maturity.ShouldBe(PlantMaturity.Sprout);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Dying)]
	[InlineData(PlantHealth.Stable)]
	[InlineData(PlantHealth.Improving)]
	[InlineData(PlantHealth.Thriving)]
	public void ctor_GeneralCall_SetsHealthToGivenPlantHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var wheat = new Wheat(map, 0, 0, PlantMaturity.Sprout, expectedHealth);

		/// Assert
		wheat.Health.ShouldBe(expectedHealth);
	}
}