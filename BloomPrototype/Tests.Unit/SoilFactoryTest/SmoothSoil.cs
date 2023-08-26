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
	[InlineData(SoilFertility.Dead, SoilFertility.Dead)]
	[InlineData(SoilFertility.Dead, SoilFertility.Thriving)]
	[InlineData(SoilFertility.Alive, SoilFertility.Overgrown)]
	[InlineData(SoilFertility.Thriving, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Dead)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilFertilityToAverageFertility(SoilFertility firstFertility, SoilFertility secondFertility)
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

		var expectedFertilityValue = (int)SoilFertility.Dead + (int)firstFertility + (int)secondFertility;
		expectedFertilityValue /= 3;

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.Fertility.ShouldBe((SoilFertility)expectedFertilityValue);
	}

	[Theory]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Wet)]
	[InlineData(SoilWaterLevel.Moist, SoilWaterLevel.Flooded)]
	[InlineData(SoilWaterLevel.Wet, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Parched)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilWaterLevelToAverageWaterLevel(SoilWaterLevel firstWaterLevel, SoilWaterLevel secondWaterLevel)
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

		var expectedWaterLevelValue = (int)SoilWaterLevel.Parched + (int)firstWaterLevel + (int)secondWaterLevel;
		expectedWaterLevelValue /= 3;

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.WaterLevel.ShouldBe((SoilWaterLevel)expectedWaterLevelValue);
	}

	[Theory]
	[InlineData(SoilRetention.Dust, SoilRetention.Dust)]
	[InlineData(SoilRetention.Dust, SoilRetention.Tight)]
	[InlineData(SoilRetention.Holding, SoilRetention.Packed)]
	[InlineData(SoilRetention.Tight, SoilRetention.Loose)]
	[InlineData(SoilRetention.Packed, SoilRetention.Loose)]
	[InlineData(SoilRetention.Packed, SoilRetention.Dust)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilRetentionToAverageRetention(SoilRetention firstRetention, SoilRetention secondRetention)
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

		var expectedRetentionValue = (int)SoilRetention.Dust + (int)firstRetention + (int)secondRetention;
		expectedRetentionValue /= 3;

		/// Act
		factory.SmoothSoil(soil, contextSoils);

		/// Assert
		soil.Retention.ShouldBe((SoilRetention)expectedRetentionValue);
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
