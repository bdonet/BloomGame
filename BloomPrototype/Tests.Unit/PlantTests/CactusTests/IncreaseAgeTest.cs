using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
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
	public void IncreaseAge_PlantIsNotOld_IncreasesMaturityByOneLevel(PlantMaturity originalMaturity, PlantMaturity expectedMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		var cactus = new Cactus(map, 0, 0, originalMaturity, PlantHealth.Stable);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(expectedMaturity);
	}

	[Fact]
	public void IncreaseAge_PlantIsOld_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Old, PlantHealth.Stable);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_PlantIsOld_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

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
	public void IncreaseAge_SoilConditionsAreIdealAndPlantIsNotDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth)
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

	[Theory]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Wet, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Tight, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	public void IncreaseAge_SoilConditionsAreNotIdealAndPlantIsNotDead_DecreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth, SoilRetention retention, SoilWaterLevel waterLevel, SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have terrible conditions for a cactus
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Seedling, originalHealth);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreNotIdealAndPlantIsDead_DoesNotChangePlanthHealth()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the terrible conditions for a cactus
		soil.Retention = SoilRetention.Packed;
		soil.WaterLevel = SoilWaterLevel.Flooded;
		soil.Fertility = SoilFertility.Overgrown;

		var cactus = new Cactus(map, 0, 0, PlantMaturity.Seedling, PlantHealth.Dead);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(PlantHealth.Dead);
	}
}
