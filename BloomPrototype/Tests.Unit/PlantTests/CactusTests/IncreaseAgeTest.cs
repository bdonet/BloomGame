using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.CactusTests;

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
		var cactus = new Cactus(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(expectedMaturity);
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
		var cactus = new Cactus(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								Cactus.MaxDaysInEachMaturity / 2,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.DaysInCurrentMaturity.ShouldBe(0);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(PlantHealth.Dead);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_IncreasesPlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Cactus.MaxDaysInEachMaturity,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Seedling);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_DoesNotRollForMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();
		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Cactus.MaxDaysInEachMaturity,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt), Occurs.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Cactus.MaxDaysInEachMaturity / 2)]
	[InlineData(Cactus.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForLessThanOneFifthOfLifespanDays_GetsRandomNumberUpToDifferenceBetweenDaysInCurrentMaturityAndOneFifthOfLifespanDays(int expectedDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								expectedDaysInMaturity,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(1, Cactus.MaxDaysInEachMaturity - expectedDaysInMaturity));
	}

	[Fact]
	public void IncreaseAge_MaturityRollFails_DoesNotIncreasePlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Maturity.ShouldBe(PlantMaturity.Sprout);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Cactus.MaxDaysInEachMaturity / 2)]
	[InlineData(Cactus.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_MaturityRollFails_IncrementsDaysInCurrentMaturity(int originalDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								originalDaysInMaturity,
								random);

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.DaysInCurrentMaturity.ShouldBe(originalDaysInMaturity + 1);
	}

	[Theory]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Packed, SoilWaterLevel.Moist, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Dying, SoilRetention.Packed, SoilWaterLevel.Flooded, SoilFertility.Overgrown)]
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

		// Set the soil to have terrible conditions for a cactus
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Seedling,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsDead_DoesNotChangePlanthHealth()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the terrible conditions for a cactus
		soil.Retention = SoilRetention.Packed;
		soil.WaterLevel = SoilWaterLevel.Flooded;
		soil.Fertility = SoilFertility.Overgrown;

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Seedling,
								PlantHealth.Dead,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(PlantHealth.Dead);
	}

	[Theory]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Packed, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Stable, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	[InlineData(PlantHealth.Dying, PlantHealth.Stable, SoilRetention.Loose, SoilWaterLevel.Dry, SoilFertility.Struggling)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsNotDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth, SoilRetention retention, SoilWaterLevel waterLevel, SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have acceptable conditions for a cactus
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Seedling,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsDeadOrThriving_DoesNotChangePlanthHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the acceptable conditions for a cactus
		soil.Retention = Cactus.SoilRetentionPreference;
		soil.WaterLevel = Cactus.SoilWaterLevelPreference;
		soil.Fertility = Cactus.SoilFertilityPreference;

		var cactus = new Cactus(map,
								0,
								0,
								PlantMaturity.Seedling,
								expectedHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		cactus.IncreaseAge();

		/// Assert
		cactus.Health.ShouldBe(expectedHealth);
	}
}
