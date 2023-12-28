using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.CactusTests;

public class ctorTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void ctor_GeneralCall_SetsCactusLocationToGivenLocationXAndLocationY(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var coordinate = new MapCoordinate(expectedX, expectedY, map);

		/// Act
		var cactus = new Cactus(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Assert
		cactus.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var cactus = new Cactus(map, 0, 0, PlantMaturity.Seedling);

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Seedling);
	}
}
