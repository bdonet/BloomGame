using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class WaterSoilTest
{
	[Fact]
	public void WaterSoil_PlayerHasAtLeastOneAction_DecrementsPlayerActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.WaterSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Theory]
	[InlineData(1, SoilWaterLevel.Dry)]
	[InlineData(2, SoilWaterLevel.Moist)]
	[InlineData(3, SoilWaterLevel.Wet)]
	[InlineData(4, SoilWaterLevel.Flooded)]
	public void WaterSoil_PlayerHasAtLeastOneAction_PassesGivenLevelsToSoil(int levelsToIncrease,
																			SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		map.GetSoil(0, 0).WaterLevel = SoilWaterLevel.Parched;

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.WaterSoil(levelsToIncrease);

		/// Assert
		map.GetSoil(0, 0).WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Fact]
	public void WaterSoil_PlayerHasNoActions_DoesNotChangeActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.WaterSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Fact]
	public void WaterSoil_PlayerHasNoActions_DoesNotChangeSoilWaterLevel()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		map.GetSoil(1, 1).WaterLevel = SoilWaterLevel.Parched;

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.WaterSoil(1);

		/// Assert
		map.GetSoil(1, 1).WaterLevel.ShouldBe(SoilWaterLevel.Parched);
	}
}
