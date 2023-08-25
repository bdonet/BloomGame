using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Tests.Unit.SoilGeneratorTest;
public class GenerateSoilTest
{
	[Fact]
	public void GenerateSoil_GeneralCall_ReturnsNewSoil()
	{
		/// Arrange
		var generator = new SoilGenerator(Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = generator.Generate();

		/// Assert
		result.ShouldNotBeNull();
	}

	[Fact]
	public void GenerateSoil_GeneralCall_CallsRandomNumberGeneratorWithSoilFertilityBounds()
	{
		/// Arrange
		var random = Mock.Create<IRandomNumberGenerator>();

		var generator = new SoilGenerator(random);

		/// Act
		generator.Generate();

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

		var generator = new SoilGenerator(random);

		/// Act
		var result = generator.Generate();

		/// Assert
		result.Fertility.ShouldBe(expectedFertility);
	}
}
