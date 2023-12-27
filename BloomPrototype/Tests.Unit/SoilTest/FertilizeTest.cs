using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Unit.SoilTest;

public class FertilizeTest
{
	[Theory]
	[InlineData(2, SoilFertility.Alive)]
	[InlineData(3, SoilFertility.Thriving)]
	[InlineData(4, SoilFertility.Overgrown)]
	public void Fertilize_FertilizingMinimumSoil_IncreasesSoilFertilityByGivenLevels(int levelsToIncrease,
																					SoilFertility expectedFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = SoilFertility.Dead };

		/// Act
		soil.Fertilize(levelsToIncrease);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(SoilFertility.Dead, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Struggling, SoilFertility.Alive)]
	[InlineData(SoilFertility.Alive, SoilFertility.Thriving)]
	[InlineData(SoilFertility.Thriving, SoilFertility.Overgrown)]
	public void Fertilize_FertilizingNonMaxedSoilByOneLevel_IncreasesSoilFertilityByOneLevel(SoilFertility originalFertility,
																							SoilFertility expectedFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = originalFertility };

		/// Act
		soil.Fertilize(1);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(1, SoilFertility.Overgrown)]
	[InlineData(2, SoilFertility.Thriving)]
	[InlineData(3, SoilFertility.Alive)]
	[InlineData(4, SoilFertility.Struggling)]
	[InlineData(5, SoilFertility.Dead)]
	public void Fertilize_FertilizingSoilBeyondMaxFertility_SetsSoilFertilityToMax(int levelsToIncrease,
																				SoilFertility originalFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = originalFertility };

		/// Act
		soil.Fertilize(levelsToIncrease);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Overgrown);
	}

	[Theory]
	[InlineData(-1, SoilFertility.Thriving)]
	[InlineData(-2, SoilFertility.Alive)]
	[InlineData(-3, SoilFertility.Struggling)]
	[InlineData(-4, SoilFertility.Dead)]
	public void Fertilize_WorseningMaximumSoil_DecreasesSoilFertilityByGivenLevels(int levelsToIncrease,
																					SoilFertility expectedFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = SoilFertility.Overgrown };

		/// Act
		soil.Fertilize(levelsToIncrease);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Thriving)]
	[InlineData(SoilFertility.Thriving, SoilFertility.Alive)]
	[InlineData(SoilFertility.Alive, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Struggling, SoilFertility.Dead)]
	public void Fertilize_WorseningNonMinSoilByOneLevel_DecreasesSoilFertilityByOneLevel(SoilFertility originalFertility,
																							SoilFertility expectedFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = originalFertility };

		/// Act
		soil.Fertilize(-1);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(-5, SoilFertility.Overgrown)]
	[InlineData(-4, SoilFertility.Thriving)]
	[InlineData(-3, SoilFertility.Alive)]
	[InlineData(-2, SoilFertility.Struggling)]
	[InlineData(-1, SoilFertility.Dead)]
	public void Fertilize_WorseningSoilBeyondMinFertility_SetsSoilFertilityToMin(int levelsToIncrease,
																				SoilFertility originalFertility)
	{
		/// Arrange
		var soil = new Soil { Fertility = originalFertility };

		/// Act
		soil.Fertilize(levelsToIncrease);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Dead);
	}
}
