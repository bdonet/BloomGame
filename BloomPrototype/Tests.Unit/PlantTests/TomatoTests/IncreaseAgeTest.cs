using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.TomatoTests;

public class IncreaseAgeTest
{
	[Theory]
	[InlineData(PlantMaturity.Sprout, PlantMaturity.Seedling)]
	[InlineData(PlantMaturity.Seedling, PlantMaturity.Established)]
	[InlineData(PlantMaturity.Established, PlantMaturity.Mature)]
	[InlineData(PlantMaturity.Mature, PlantMaturity.Old)]
	public void IncreaseAge_PlantIsNotOldAndMaturityRollSucceeds_IncreasesMaturityByOneLevel(PlantMaturity originalMaturity,
																	PlantMaturity expectedMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tomato = new Tomato(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Maturity.ShouldBe(expectedMaturity);
	}

	[Theory]
	[InlineData(PlantMaturity.Sprout)]
	[InlineData(PlantMaturity.Seedling)]
	[InlineData(PlantMaturity.Established)]
	[InlineData(PlantMaturity.Mature)]
	[InlineData(PlantMaturity.Old)]
	public void IncreaseAge_MaturityRollSucceeds_SetsDaysInCurrentMaturityTo0(PlantMaturity originalMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tomato = new Tomato(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								Tomato.MaxDaysInEachMaturity / 2,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.DaysInCurrentMaturity.ShouldBe(0);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Health.ShouldBe(PlantHealth.Dead);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_IncreasesPlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Tomato.MaxDaysInEachMaturity,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Maturity.ShouldBe(PlantMaturity.Seedling);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_DoesNotRollForMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();
		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Tomato.MaxDaysInEachMaturity,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt), Occurs.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Tomato.MaxDaysInEachMaturity / 2)]
	[InlineData(Tomato.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForLessThanOneFifthOfLifespanDays_GetsRandomNumberUpToDifferenceBetweenDaysInCurrentMaturityAndOneFifthOfLifespanDays(int expectedDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								expectedDaysInMaturity,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(1, Tomato.MaxDaysInEachMaturity - expectedDaysInMaturity));
	}

	[Fact]
	public void IncreaseAge_MaturityRollFails_DoesNotIncreasePlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Maturity.ShouldBe(PlantMaturity.Sprout);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Tomato.MaxDaysInEachMaturity / 2)]
	[InlineData(Tomato.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_MaturityRollFails_IncrementsDaysInCurrentMaturity(int originalDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								originalDaysInMaturity,
								random);

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.DaysInCurrentMaturity.ShouldBe(originalDaysInMaturity + 1);
	}

	[Theory]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Struggling, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsNotDead_DecreasesPlantHealthOneLevel(PlantHealth originalHealth,
																									PlantHealth expectedHealth,
																									SoilRetention retention,
																									SoilWaterLevel waterLevel,
																									SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have terrible conditions for a tomato
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Seedling,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Health.ShouldBe(expectedHealth);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsDead_DoesNotChangePlantHealth()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the terrible conditions for a tomato
		soil.Retention = SoilRetention.Packed;
		soil.WaterLevel = SoilWaterLevel.Flooded;
		soil.Fertility = SoilFertility.Overgrown;

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Seedling,
								PlantHealth.Dead,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Health.ShouldBe(PlantHealth.Dead);
	}

	[Theory]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Struggling, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsNotDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth, SoilRetention retention, SoilWaterLevel waterLevel, SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have acceptable conditions for a tomato
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Seedling,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsDeadOrThriving_DoesNotChangePlantHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the acceptable conditions for a tomato
		soil.Retention = Tomato.SoilRetentionPreference;
		soil.WaterLevel = Tomato.SoilWaterLevelPreference;
		soil.Fertility = Tomato.SoilFertilityPreference;

		var tomato = new Tomato(map,
								0,
								0,
								PlantMaturity.Seedling,
								expectedHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tomato.IncreaseAge();

		/// Assert
		tomato.Health.ShouldBe(expectedHealth);
	}
}
