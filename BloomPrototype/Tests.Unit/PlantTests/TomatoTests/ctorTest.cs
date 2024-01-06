using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
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
		var coordinate = new MapCoordinate(expectedX, expectedY, map);

		/// Act
		var tomato = new Tomato(map,
									expectedX,
									expectedY,
									PlantMaturity.Sprout,
									PlantHealth.Stable,
									0,
									Mock.Create<IRandomNumberGenerator>());

		/// Assert
		tomato.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var tomato = new Tomato(map,
									0,
									0,
									PlantMaturity.Sprout,
									PlantHealth.Stable,
									0,
									Mock.Create<IRandomNumberGenerator>());

		/// Assert
		tomato.Maturity.ShouldBe(PlantMaturity.Sprout);
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
		var tomato = new Tomato(map,
									0,
									0,
									PlantMaturity.Sprout,
									expectedHealth,
									0,
									Mock.Create<IRandomNumberGenerator>());

		/// Assert
		tomato.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(64)]
	[InlineData(int.MaxValue)]
	public void ctor_GeneralCall_SetsDaysInCurrentMaturityToGivenNumber(int expectedDays)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var tomato = new Tomato(map,
									0,
									0,
									PlantMaturity.Sprout,
									PlantHealth.Stable,
									expectedDays,
									Mock.Create<IRandomNumberGenerator>());

		/// Assert
		tomato.DaysInCurrentMaturity.ShouldBe(expectedDays);
	}
}
