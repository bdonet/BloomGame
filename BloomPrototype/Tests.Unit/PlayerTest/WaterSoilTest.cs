using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class WaterSoilTest
{
	[Theory]
	[InlineData(1, SoilWaterLevel.Dry)]
	[InlineData(2, SoilWaterLevel.Moist)]
	[InlineData(3, SoilWaterLevel.Wet)]
	[InlineData(4, SoilWaterLevel.Flooded)]
	public void WaterSoil_GeneralCall_PassesGivenLevelsToSoil(int levelsToIncrease, SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		map.GetSoil(1, 1).WaterLevel = SoilWaterLevel.Parched;

		var player = new Player(map);

		/// Act
		player.WaterSoil(levelsToIncrease);

		/// Assert
		map.GetSoil(1, 1).WaterLevel.ShouldBe(expectedWaterLevel);
	}
}
