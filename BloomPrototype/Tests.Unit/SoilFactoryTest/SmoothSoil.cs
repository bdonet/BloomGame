using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using Telerik.JustMock;

namespace Tests.Unit.SoilFactoryTest;
public class SmoothSoil
{
	[Theory]
	[InlineData(SoilFertility.Dead, SoilFertility.Dead, SoilFertility.Dead)]
	[InlineData(SoilFertility.Dead, SoilFertility.Thriving, SoilFertility.Dead)]
	[InlineData(SoilFertility.Alive, SoilFertility.Overgrown, SoilFertility.Alive)]
	[InlineData(SoilFertility.Thriving, SoilFertility.Struggling, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Struggling, SoilFertility.Alive)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Dead, SoilFertility.Struggling)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilFertilityToAverageFertilityWithStandardRoundingAndWeightedExtremes(SoilFertility firstFertility, SoilFertility secondFertility, SoilFertility expectedFertility)
	{
		/// Arrange
		var factory = new SoilFactory(Mock.Create<IRandomNumberGenerator>());

		var soil = new Soil
		{
			Fertility = SoilFertility.Dead,
		};

		var contextSoils = new List<Soil>
		{
			new Soil
			{
				Fertility = firstFertility,
			},
			new Soil
			{
				Fertility = secondFertility,
			}
		};

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Parched, SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Wet, SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Moist, SoilWaterLevel.Flooded, SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Wet, SoilWaterLevel.Dry, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Dry, SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Parched, SoilWaterLevel.Dry)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilWaterLevelToAverageWaterLevelWithStandardRoundingAndWeightedExtremes(SoilWaterLevel firstWaterLevel, SoilWaterLevel secondWaterLevel, SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var factory = new SoilFactory(Mock.Create<IRandomNumberGenerator>());

		var soil = new Soil
		{
			WaterLevel = SoilWaterLevel.Parched
		};

		var contextSoils = new List<Soil>
		{
			new Soil
			{
				WaterLevel = firstWaterLevel,
			},
			new Soil
			{
				WaterLevel = secondWaterLevel,
			}
		};

		var expectedWaterLevelValue = (double)SoilWaterLevel.Parched + (double)firstWaterLevel + (double)secondWaterLevel;
		expectedWaterLevelValue /= 3;
		expectedWaterLevelValue = Math.Round(expectedWaterLevelValue);

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(SoilRetention.Dust, SoilRetention.Dust, SoilRetention.Dust)]
	[InlineData(SoilRetention.Dust, SoilRetention.Tight, SoilRetention.Dust)]
	[InlineData(SoilRetention.Holding, SoilRetention.Packed, SoilRetention.Holding)]
	[InlineData(SoilRetention.Tight, SoilRetention.Loose, SoilRetention.Loose)]
	[InlineData(SoilRetention.Packed, SoilRetention.Loose, SoilRetention.Holding)]
	[InlineData(SoilRetention.Packed, SoilRetention.Dust, SoilRetention.Loose)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilRetentionToAverageRetentionWithStandardRoundingAndWeightedExtremes(SoilRetention firstRetention, SoilRetention secondRetention, SoilRetention expectedRetention)
	{
		/// Arrange
		var factory = new SoilFactory(Mock.Create<IRandomNumberGenerator>());

		var soil = new Soil
		{
			Retention = SoilRetention.Dust
		};

		var contextSoils = new List<Soil>
		{
			new Soil
			{
				Retention = firstRetention,
			},
			new Soil
			{
				Retention = secondRetention,
			}
		};

		var expectedRetentionValue = (double)SoilRetention.Dust + (double)firstRetention + (double)secondRetention;
		expectedRetentionValue /= 3;
		expectedRetentionValue = Math.Round(expectedRetentionValue);

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.Retention.ShouldBe(expectedRetention);
	}

	[Fact]
	public void SmoothSoil_ContextSoilsIsNull_DoesNotChangeCurrentSoil()
	{
		/// Arrange
		var factory = new SoilFactory(Mock.Create<RandomNumberGenerator>());

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Dust
		};

		/// Act
		factory.SmoothSoil(soil, null);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Alive);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Dry);
		soil.Retention.ShouldBe(SoilRetention.Dust);
	}
}
