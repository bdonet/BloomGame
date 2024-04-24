using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.WheatTests;

public class IncreaseAgeTest
{
	[Theory]
	[InlineData(PlantMaturity.Sprout, PlantMaturity.Infant)]
	[InlineData(PlantMaturity.Infant, PlantMaturity.Young)]
	[InlineData(PlantMaturity.Young, PlantMaturity.Mature)]
	[InlineData(PlantMaturity.Mature, PlantMaturity.Old)]
	public void IncreaseAge_PlantIsNotOldAndMaturityRollSucceeds_IncreasesMaturityByOneLevel(PlantMaturity originalMaturity,
																	PlantMaturity expectedMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var wheat = new Wheat(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Maturity.ShouldBe(expectedMaturity);
	}

	[Theory]
	[InlineData(PlantMaturity.Sprout)]
	[InlineData(PlantMaturity.Infant)]
	[InlineData(PlantMaturity.Young)]
	[InlineData(PlantMaturity.Mature)]
	[InlineData(PlantMaturity.Old)]
	public void IncreaseAge_MaturityRollSucceeds_SetsDaysInCurrentMaturityTo0(PlantMaturity originalMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var wheat = new Wheat(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								Wheat.MaxDaysInEachMaturity / 2,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.DaysInCurrentMaturity.ShouldBe(0);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Health.ShouldBe(PlantHealth.Dead);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_IncreasesPlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Wheat.MaxDaysInEachMaturity,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Maturity.ShouldBe(PlantMaturity.Infant);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_DoesNotRollForMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();
		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Wheat.MaxDaysInEachMaturity,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt), Occurs.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Wheat.MaxDaysInEachMaturity / 2)]
	[InlineData(Wheat.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForLessThanOneFifthOfLifespanDays_GetsRandomNumberUpToDifferenceBetweenDaysInCurrentMaturityAndOneFifthOfLifespanDays(int expectedDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								expectedDaysInMaturity,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(1, Wheat.MaxDaysInEachMaturity - expectedDaysInMaturity));
	}

	[Fact]
	public void IncreaseAge_MaturityRollFails_DoesNotIncreasePlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Maturity.ShouldBe(PlantMaturity.Sprout);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Wheat.MaxDaysInEachMaturity / 2)]
	[InlineData(Wheat.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_MaturityRollFails_IncrementsDaysInCurrentMaturity(int originalDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								originalDaysInMaturity,
								random);

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.DaysInCurrentMaturity.ShouldBe(originalDaysInMaturity + 1);
	}

	[Theory]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Loose, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Struggling, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Flooded, SoilFertility.Dead)]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsNotDead_DecreasesPlantHealthOneLevel(PlantHealth originalHealth,
																									PlantHealth expectedHealth,
																									SoilRetention retention,
																									SoilWaterLevel waterLevel,
																									SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have terrible conditions for a wheat
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Infant,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Health.ShouldBe(expectedHealth);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsDead_DoesNotChangePlantHealth()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the terrible conditions for a wheat
		soil.Retention = SoilRetention.Packed;
		soil.WaterLevel = SoilWaterLevel.Flooded;
		soil.Fertility = SoilFertility.Overgrown;

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Infant,
								PlantHealth.Dead,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Health.ShouldBe(PlantHealth.Dead);
	}

	[Theory]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Holding, SoilWaterLevel.Flooded, SoilFertility.Alive)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Holding, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Stable, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	[InlineData(PlantHealth.Dying, PlantHealth.Struggling, SoilRetention.Holding, SoilWaterLevel.Dry, SoilFertility.Alive)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsNotDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth, SoilRetention retention, SoilWaterLevel waterLevel, SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have acceptable conditions for a wheat
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Infant,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsDeadOrThriving_DoesNotChangePlantHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the acceptable conditions for a wheat
		soil.Retention = Wheat.SoilRetentionPreference;
		soil.WaterLevel = Wheat.SoilWaterLevelPreference;
		soil.Fertility = Wheat.SoilFertilityPreference;

		var wheat = new Wheat(map,
								0,
								0,
								PlantMaturity.Infant,
								expectedHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		wheat.IncreaseAge();

		/// Assert
		wheat.Health.ShouldBe(expectedHealth);
	}
}
