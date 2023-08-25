using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using Telerik.JustMock;

namespace Tests.Unit.SoilFactoryTest;
public class GenerateSoilTest
{
	[Fact]
	public void GenerateSoil_GeneralCall_CallsRandomNumberGeneratorWithSoilFertilityBounds()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		/// Act
		factory.GenerateSoil();

		/// Assert
		Mock.Assert(() => random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown), Occurs.Once());
	}

	[Theory]
	[InlineData(SoilFertility.Dead)]
	[InlineData(SoilFertility.Struggling)]
	[InlineData(SoilFertility.Alive)]
	[InlineData(SoilFertility.Thriving)]
	[InlineData(SoilFertility.Overgrown)]
	public void GenerateSoil_GeneralCall_UsesRandomNumberGeneratorToReturnSoilWithRandomFertility(SoilFertility expectedFertility)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns((int)expectedFertility);

		var factory = new SoilFactory(random);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		result.Fertility.ShouldBe(expectedFertility);
	}

	[Fact]
	public void GenerateSoil_GeneralCall_CallsRandomNumberGeneratorWithSoilWaterLevelBounds()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		/// Act
		factory.GenerateSoil();

		/// Assert
		Mock.Assert(() => random.GenerateInt((int)SoilWaterLevel.Parched, (int)SoilWaterLevel.Flooded), Occurs.Once());
	}

	[Theory]
	[InlineData(SoilWaterLevel.Parched)]
	[InlineData(SoilWaterLevel.Dry)]
	[InlineData(SoilWaterLevel.Moist)]
	[InlineData(SoilWaterLevel.Wet)]
	[InlineData(SoilWaterLevel.Flooded)]
	public void GenerateSoil_GeneralCall_UsesRandomNumberGeneratorToReturnSoilWithRandomWaterLevel(SoilWaterLevel expectedWaterLevel)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns((int)expectedWaterLevel);

		var factory = new SoilFactory(random);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		result.WaterLevel.ShouldBe(expectedWaterLevel);
	}

	[Fact]
	public void GenerateSoil_GeneralCall_CallsRandomNumberGeneratorWithSoilRetentionBounds()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var factory = new SoilFactory(random);

		/// Act
		factory.GenerateSoil();

		/// Assert
		Mock.Assert(() => random.GenerateInt((int)SoilRetention.Dust, (int)SoilRetention.Packed), Occurs.Once());
	}

	[Theory]
	[InlineData(SoilRetention.Dust)]
	[InlineData(SoilRetention.Loose)]
	[InlineData(SoilRetention.Holding)]
	[InlineData(SoilRetention.Tight)]
	[InlineData(SoilRetention.Packed)]
	public void GenerateSoil_GeneralCall_UsesRandomNumberGeneratorToReturnSoilWithRandomRetention(SoilRetention expectedRetention)
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();
		Mock.Arrange(() => random.GenerateInt(Arg.AnyInt, Arg.AnyInt)).Returns((int)expectedRetention);

		var factory = new SoilFactory(random);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		result.Retention.ShouldBe(expectedRetention);
	}
}
