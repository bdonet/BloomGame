using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Telerik.JustMock;

namespace Tests.Unit.SoilFactoryTest;
public class SmoothSoil
{
	[Theory]
	[InlineData(SoilFertility.Dead, SoilFertility.Dead, SoilFertility.Dead)]
	[InlineData(SoilFertility.Dead, SoilFertility.Thriving, SoilFertility.Dead)]
	[InlineData(SoilFertility.Alive, SoilFertility.Overgrown, SoilFertility.Alive)]
	[InlineData(SoilFertility.Thriving, SoilFertility.Struggling, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Struggling, SoilFertility.Struggling)]
	[InlineData(SoilFertility.Overgrown, SoilFertility.Dead, SoilFertility.Dead)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilFertilityToAverageFertilityWithStandardRoundingAndWeightedExtremes(SoilFertility firstFertility, SoilFertility secondFertility, SoilFertility expectedFertility)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(3);

		var factory = new SoilFactory(random);

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
		factory.SmoothSoil(soil, contextSoils, 2, 20);

		/// Assert
		soil.Fertility.ShouldBe(expectedFertility);
	}

	[Theory]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Parched, SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Parched, SoilWaterLevel.Wet, SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Moist, SoilWaterLevel.Flooded, SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Wet, SoilWaterLevel.Dry, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Dry, SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Flooded, SoilWaterLevel.Parched, SoilWaterLevel.Parched)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilWaterLevelToAverageWaterLevelWithStandardRoundingAndWeightedExtremes(SoilWaterLevel firstWaterLevel, SoilWaterLevel secondWaterLevel, SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(3);

		var factory = new SoilFactory(random);

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

		/// Act
		factory.SmoothSoil(soil, contextSoils, 2, 20);

		/// Assert
		soil.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Theory]
	[InlineData(SoilRetention.Dust, SoilRetention.Dust, SoilRetention.Dust)]
	[InlineData(SoilRetention.Dust, SoilRetention.Tight, SoilRetention.Dust)]
	[InlineData(SoilRetention.Holding, SoilRetention.Packed, SoilRetention.Holding)]
	[InlineData(SoilRetention.Tight, SoilRetention.Loose, SoilRetention.Loose)]
	[InlineData(SoilRetention.Packed, SoilRetention.Loose, SoilRetention.Loose)]
	[InlineData(SoilRetention.Packed, SoilRetention.Dust, SoilRetention.Dust)]
	public void SmoothSoil_ContextSoilsAreGiven_ChangesSoilRetentionToAverageRetentionWithStandardRoundingAndWeightedExtremes(SoilRetention firstRetention, SoilRetention secondRetention, SoilRetention expectedRetention)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(3);

		var factory = new SoilFactory(random);

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

		/// Act
		factory.SmoothSoil(soil, contextSoils, 2, 20);

		/// Assert
		soil.Retention.ShouldBe(expectedRetention);
	}

	[Fact]
	public void SmoothSoil_ContextSoilsIsNull_DoesNotChangeCurrentSoil()
	{
		/// Arrange
		var factory = new SoilFactory(Mock.Create<IRandomNumberGenerator>());

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Dust
		};

		/// Act
		factory.SmoothSoil(soil, null, 2, 20);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Alive);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Dry);
		soil.Retention.ShouldBe(SoilRetention.Dust);
	}

	[Fact]
	public void SmoothSoil_RandomReturns1_OffsetsResultSoilWithNegative1()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(1);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 20);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Struggling);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Parched);
		soil.Retention.ShouldBe(SoilRetention.Holding);
	}

	[Fact]
	public void SmoothSoil_RandomReturns5_OffsetsResultSoilWith1()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(5);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 20);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Thriving);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Moist);
		soil.Retention.ShouldBe(SoilRetention.Packed);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(3)]
	[InlineData(4)]
	public void SmoothSoil_RandomReturnsBetween1And5_DoesNotOffsetSoil(int expectedRandom)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(expectedRandom);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 20);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Alive);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Dry);
		soil.Retention.ShouldBe(SoilRetention.Tight);
	}

	[Theory]
	[InlineData(5, 20)]
	[InlineData(10, 10)]
	[InlineData(20, 5)]
	[InlineData(50, 2)]
	public void SmoothSoil_GeneralCall_OffsetGeneratesWithGivenPercentChanceOfNegative1Or1(int offsetPercentChance, int maxRandomValue)
	{
		/// Arrange
		int? actualLowerBound = null;
		int? actualUpperBound = null;

		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).DoInstead<int, int>((lower, upper) =>
		{
			actualLowerBound = lower;
			actualUpperBound = upper;
		}).Returns(maxRandomValue);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, offsetPercentChance);

		/// Assert
		actualLowerBound.ShouldBe(1);
		actualUpperBound.ShouldBe(maxRandomValue);
		Mock.Assert(() => random.GenerateInt(1, maxRandomValue), Occurs.Exactly(3));
	}

	[Theory]
	[InlineData(3, 33)]
	[InlineData(33, 3)]
	[InlineData(8, 12)]
	public void SmoothSoil_OffsetChanceIsNotWholeNumber_OffsetChanceIsFloored(int offsetPercentChance, int maxRandomValue)
	{
		/// Arrange
		int? actualLowerBound = null;
		int? actualUpperBound = null;

		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).DoInstead<int, int>((lower, upper) =>
		{
			actualLowerBound = lower;
			actualUpperBound = upper;
		}).Returns(maxRandomValue);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, offsetPercentChance);

		/// Assert
		actualLowerBound.ShouldBe(1);
		actualUpperBound.ShouldBe(maxRandomValue);
		Mock.Assert(() => random.GenerateInt(1, maxRandomValue), Occurs.Exactly(3));
	}

	[Theory]
	[InlineData(51)]
	[InlineData(80)]
	[InlineData(66)]
	public void SmoothSoil_OffsetChanceIsOver50_ThrowsArgumentException(int offsetPercentChance)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		var exception = Record.Exception(() => factory.SmoothSoil(soil, new List<Soil>(), 2, offsetPercentChance));

		/// Assert
		exception.ShouldBeOfType<ArgumentException>();
		exception.Message.ShouldContain("soil");
		exception.Message.ShouldContain("offset");
		exception.Message.ShouldContain("chance");
		exception.Message.ShouldContain("over");
		exception.Message.ShouldContain("50");
	}

	[Fact]
	public void SmoothSoil_OffsetChanceIs0_DoesNotCallRandomNumberGenerator()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 0);

		/// Assert
		Mock.Assert(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt), Occurs.Never());
	}

	[Fact]
	public void SmoothSoil_OffsetChanceIs0_DoesNotOffsetSoil()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 0);

		/// Assert
		soil.Fertility.ShouldBe(SoilFertility.Alive);
		soil.WaterLevel.ShouldBe(SoilWaterLevel.Dry);
		soil.Retention.ShouldBe(SoilRetention.Tight);
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(6)]
	[InlineData(7)]
	[InlineData(int.MaxValue)]
	public void SmoothSoil_RandomNumberGeneratorProvidesNumberOutOfBounds_ThrowsArgumentException(int unexpectedNumber)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(unexpectedNumber);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = SoilFertility.Alive,
			WaterLevel = SoilWaterLevel.Dry,
			Retention = SoilRetention.Tight
		};

		/// Act
		var exception = Record.Exception(() => factory.SmoothSoil(soil, new List<Soil>(), 2, 20));

		/// Assert
		exception.ShouldBeOfType<ArgumentException>();
		exception.Message.ShouldContain("unsupported");
		exception.Message.ShouldContain("random");
		exception.Message.ShouldContain("soil");
		exception.Message.ShouldContain("offset");
	}

	[Theory]
	[InlineData(1)]
	[InlineData(5)]
	public void SmoothSoil_OffsetSoilGoesBeyondSoilBounds_ReturnsSoilOnBounds(int expectedRandom)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns(expectedRandom);

		var factory = new SoilFactory(random);

		var soil = new Soil
		{
			Fertility = (SoilFertility)expectedRandom,
			WaterLevel = (SoilWaterLevel)expectedRandom,
			Retention = (SoilRetention)expectedRandom
		};

		/// Act
		factory.SmoothSoil(soil, new List<Soil>(), 2, 20);

		/// Assert
		soil.Fertility.ShouldBe((SoilFertility)expectedRandom);
		soil.WaterLevel.ShouldBe((SoilWaterLevel)expectedRandom);
		soil.Retention.ShouldBe((SoilRetention)expectedRandom);
	}
}
