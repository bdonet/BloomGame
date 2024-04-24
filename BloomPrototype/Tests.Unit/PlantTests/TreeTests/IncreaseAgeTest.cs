using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.TreeTests;

public class IncreaseAgeTest
{
	[Theory]
	[InlineData(PlantMaturity.Sprout, PlantMaturity.Infant)]
	[InlineData(PlantMaturity.Infant, PlantMaturity.Established)]
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
		var tree = new Tree(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Maturity.ShouldBe(expectedMaturity);
	}

	[Theory]
	[InlineData(PlantMaturity.Sprout)]
	[InlineData(PlantMaturity.Infant)]
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
		var tree = new Tree(map,
								0,
								0,
								originalMaturity,
								PlantHealth.Stable,
								Tree.MaxDaysInEachMaturity / 2,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.DaysInCurrentMaturity.ShouldBe(0);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_DoesNotChangeMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Maturity.ShouldBe(PlantMaturity.Old);
	}

	[Fact]
	public void IncreaseAge_PlantIsOldAndMaturityRollSucceeds_ChangesPlantHealthToDead()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A result of 1 simulates a successful roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);
		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Old,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Health.ShouldBe(PlantHealth.Dead);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_IncreasesPlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Tree.MaxDaysInEachMaturity,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Maturity.ShouldBe(PlantMaturity.Infant);
	}

	[Fact]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForOneFifthOfLifespanDays_DoesNotRollForMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();
		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								Tree.MaxDaysInEachMaturity,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt), Occurs.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Tree.MaxDaysInEachMaturity / 2)]
	[InlineData(Tree.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_PlantHasBeenInCurrentMaturityForLessThanOneFifthOfLifespanDays_GetsRandomNumberUpToDifferenceBetweenDaysInCurrentMaturityAndOneFifthOfLifespanDays(int expectedDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var random = Mock.Create<IRandomNumberGenerator>();

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								expectedDaysInMaturity,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		Mock.Assert(() => random.GenerateInt(1, Tree.MaxDaysInEachMaturity - expectedDaysInMaturity));
	}

	[Fact]
	public void IncreaseAge_MaturityRollFails_DoesNotIncreasePlantMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								0,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Maturity.ShouldBe(PlantMaturity.Sprout);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(Tree.MaxDaysInEachMaturity / 2)]
	[InlineData(Tree.MaxDaysInEachMaturity - 1)]
	public void IncreaseAge_MaturityRollFails_IncrementsDaysInCurrentMaturity(int originalDaysInMaturity)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		// A return of anything other than 1 simulates a failed roll
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(0);

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Sprout,
								PlantHealth.Stable,
								originalDaysInMaturity,
								random);

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.DaysInCurrentMaturity.ShouldBe(originalDaysInMaturity + 1);
	}

	[Theory]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Dust, SoilWaterLevel.Dry, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Overgrown)]
	[InlineData(PlantHealth.Thriving, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Dry, SoilFertility.Dead)]
	[InlineData(PlantHealth.Improving, PlantHealth.Stable, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Struggling, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Dying, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	[InlineData(PlantHealth.Dying, PlantHealth.Dead, SoilRetention.Dust, SoilWaterLevel.Parched, SoilFertility.Dead)]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsNotDead_DecreasesPlantHealthOneLevel(PlantHealth originalHealth,
																									PlantHealth expectedHealth,
																									SoilRetention retention,
																									SoilWaterLevel waterLevel,
																									SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have terrible conditions for a tree
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Infant,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Health.ShouldBe(expectedHealth);
	}

	[Fact]
	public void IncreaseAge_SoilConditionsAreTerribleAndPlantIsDead_DoesNotChangePlantHealth()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the terrible conditions for a tree
		soil.Retention = SoilRetention.Packed;
		soil.WaterLevel = SoilWaterLevel.Flooded;
		soil.Fertility = SoilFertility.Overgrown;

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Infant,
								PlantHealth.Dead,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Health.ShouldBe(PlantHealth.Dead);
	}

	[Theory]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Dust, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Holding, SoilWaterLevel.Parched, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Improving, PlantHealth.Thriving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Dead)]
	[InlineData(PlantHealth.Stable, PlantHealth.Improving, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Struggling, PlantHealth.Stable, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	[InlineData(PlantHealth.Dying, PlantHealth.Struggling, SoilRetention.Tight, SoilWaterLevel.Moist, SoilFertility.Thriving)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsNotDeadOrThriving_IncreasesPlantHealthOneLevel(PlantHealth originalHealth, PlantHealth expectedHealth, SoilRetention retention, SoilWaterLevel waterLevel, SoilFertility fertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have acceptable conditions for a tree
		soil.Retention = retention;
		soil.WaterLevel = waterLevel;
		soil.Fertility = fertility;

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Infant,
								originalHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Health.ShouldBe(expectedHealth);
	}

	[Theory]
	[InlineData(PlantHealth.Dead)]
	[InlineData(PlantHealth.Thriving)]
	public void IncreaseAge_SoilConditionsAreAcceptableAndPlantIsDeadOrThriving_DoesNotChangePlantHealth(PlantHealth expectedHealth)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var soil = map.GetSoil(new MapCoordinate(0, 0, map));

		// Set the soil to have the acceptable conditions for a tree
		soil.Retention = Tree.SoilRetentionPreference;
		soil.WaterLevel = Tree.SoilWaterLevelPreference;
		soil.Fertility = Tree.SoilFertilityPreference;

		var tree = new Tree(map,
								0,
								0,
								PlantMaturity.Infant,
								expectedHealth,
								0,
								Mock.Create<IRandomNumberGenerator>());

		/// Act
		tree.IncreaseAge();

		/// Assert
		tree.Health.ShouldBe(expectedHealth);
	}
}
