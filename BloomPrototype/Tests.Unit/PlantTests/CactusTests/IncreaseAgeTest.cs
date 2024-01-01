using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.CactusTests;

public class IncreaseAgeTest
{
	[Theory]
	[InlineData(PlantMaturity.Sprout, PlantMaturity.Seedling)]
	[InlineData(PlantMaturity.Seedling, PlantMaturity.Established)]
	[InlineData(PlantMaturity.Established, PlantMaturity.Mature)]
	[InlineData(PlantMaturity.Mature, PlantMaturity.Old)]
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsNotOld_IncreasesMaturityByOneLevel(PlantMaturity originalMaturity, PlantMaturity expectedMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the ideal conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map, 0, 0, originalMaturity, PlantHealth.Stable);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(expectedMaturity);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsOld_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the ideal conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Old, PlantHealth.Stable);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsOld_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the ideal conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Old, PlantHealth.Stable);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(PlantHealth.Dead);
	}

	[Theory]
	[InlineData(PlantHealth.Dying, PlantHealth.Stable)]
	[InlineData(PlantHealth.Stable, PlantHealth.Improving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsNotOldOrDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the ideal conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Seedling, originalHealth);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsDeadOrThriving_DoesNotChangePlanthHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the ideal conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Seedling, expectedHealth);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}
}
