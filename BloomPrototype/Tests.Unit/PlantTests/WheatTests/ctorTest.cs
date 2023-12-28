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

		/// Act
		var wheat = new Wheat(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Assert
		wheat.Location.ShouldBe(map.GetSoil(expectedX, expectedY));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var wheat = new Wheat(map, 0, 0, PlantMaturity.Seedling);

		/// Assert
		wheat.Maturity.ShouldBe(PlantMaturity.Seedling);
	}
}
