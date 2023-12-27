using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Unit.SoilTest;

public class WaterTest
{
	[Theory]
	[InlineData(1, SoilWaterLevel.Dry)]
	[InlineData(2, SoilWaterLevel.Moist)]
	[InlineData(3, SoilWaterLevel.Wet)]
	[InlineData(4, SoilWaterLevel.Flooded)]
	public void Water_WateringMinimumSoil_IncreasesSoilWaterLevelByGivenLevels(int levelsToIncrease,
																			SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = SoilWaterLevel.Parched };

		/// Act
		soil.Water(levelsToIncrease);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Dry, SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Moist, SoilWaterLevel.Wet)]
	[InlineData(SoilWaterLevel.Wet, SoilWaterLevel.Flooded)]
	public void Water_WateringNonMaxedSoilByOneLevel_IncreasesSoilWaterLevelByOneLevel(SoilWaterLevel originalWaterLevel,
																					SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = originalWaterLevel };

		/// Act
		soil.Water(1);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(1, SoilWaterLevel.Flooded)]
	[InlineData(2, SoilWaterLevel.Wet)]
	[InlineData(3, SoilWaterLevel.Moist)]
	[InlineData(4, SoilWaterLevel.Dry)]
	[InlineData(5, SoilWaterLevel.Parched)]
	public void Water_WateringSoilBeyondMaxWaterLevel_SetsSoilWaterLevelToMax(int levelsToIncrease,
																			SoilWaterLevel originalWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = originalWaterLevel };

		/// Act
		soil.Water(levelsToIncrease);

		/// Assert
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Flooded);
	}

	[Theory]
	[InlineData(-1, SoilWaterLevel.Wet)]
	[InlineData(-2, SoilWaterLevel.Moist)]
	[InlineData(-3, SoilWaterLevel.Dry)]
	[InlineData(-4, SoilWaterLevel.Parched)]
	public void Water_DryingMaximumSoil_DecreasesSoilWaterLevelByGivenLevels(int levelsToIncrease,
																			SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = SoilWaterLevel.Flooded };

		/// Act
		soil.Water(levelsToIncrease);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Wet)]
	[InlineData(SoilWaterLevel.Wet, SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Moist, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Dry, SoilWaterLevel.Parched)]
	public void Water_DryingNonMinSoilByOneLevel_DecreasesSoilWaterLevelByOneLevel(SoilWaterLevel originalWaterLevel,
																					SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = originalWaterLevel };

		/// Act
		soil.Water(-1);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(-5, SoilWaterLevel.Flooded)]
	[InlineData(-4, SoilWaterLevel.Wet)]
	[InlineData(-3, SoilWaterLevel.Moist)]
	[InlineData(-2, SoilWaterLevel.Dry)]
	[InlineData(-1, SoilWaterLevel.Parched)]
	public void Water_DryingSoilBeyondMinWaterLevel_SetsSoilWaterLevelToMin(int levelsToIncrease,
																			SoilWaterLevel originalWaterLevel)
	{
		/// Arrange
		var soil = new Soil { WaterLevel = originalWaterLevel };

		/// Act
		soil.Water(levelsToIncrease);

		/// Assert
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Parched);
	}
}
