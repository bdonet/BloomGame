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
}
