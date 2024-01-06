using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using Telerik.JustMock;

namespace Tests.Integration.MapFactoryTest;
public class GenerateMap : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithGridFullOfGeneratedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		foreach (var item in result.GetView(new MapCoordinate(0, 0, result), new MapCoordinate(99, 99, result)))
		{
			item.ShouldNotBeNull();
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithoutAliveOrThrivingOrOvergrownSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		foreach (var item in result.GetView(new MapCoordinate(0, 0, result), new MapCoordinate(99, 99, result)))
		{
			item.Fertility.ShouldNotBe(SoilFertility.Alive);
			item.Fertility.ShouldNotBe(SoilFertility.Thriving);
			item.Fertility.ShouldNotBe(SoilFertility.Overgrown);
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithoutMoistOrWetOrFloodedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		foreach (var item in result.GetView(new MapCoordinate(0, 0, result), new MapCoordinate(99, 99, result)))
		{
			item.WaterLevel.ShouldNotBe(SoilWaterLevel.Moist);
			item.WaterLevel.ShouldNotBe(SoilWaterLevel.Wet);
			item.WaterLevel.ShouldNotBe(SoilWaterLevel.Flooded);
		}
	}
}
