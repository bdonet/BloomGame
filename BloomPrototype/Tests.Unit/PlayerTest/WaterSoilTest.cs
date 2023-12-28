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
		var coordinate = new MapCoordinate(0, 0, map);
		map.GetSoil(coordinate).WaterLevel = SoilWaterLevel.Parched;

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.WaterSoil(levelsToIncrease);

		/// Assert
		map.GetSoil(coordinate).WaterLevel.ShouldBe(expectedWaterLevel);
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
		var coordinate = new MapCoordinate(1, 1, map);
		map.GetSoil(coordinate).WaterLevel = SoilWaterLevel.Parched;

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.WaterSoil(1);

		/// Assert
		map.GetSoil(coordinate).WaterLevel.ShouldBe(SoilWaterLevel.Parched);
	}
}
