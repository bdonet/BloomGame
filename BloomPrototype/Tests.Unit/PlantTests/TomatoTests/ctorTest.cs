using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.TomatoTests;

public class ctorTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void ctor_GeneralCall_SetsTomatoLocationToGivenLocationXAndLocationY(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var tomato = new Tomato(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Assert
		tomato.Location.ShouldBe(map.GetSoil(expectedX, expectedY));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var tomato = new Tomato(map, 0, 0, PlantMaturity.Seedling);

		/// Assert
		tomato.Maturity.ShouldBe(PlantMaturity.Seedling);
	}
}
