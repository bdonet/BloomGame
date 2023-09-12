using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest;
public class GenerateMap : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithGridFullOfGeneratedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration);

		/// Act
		var result = factory.GenerateMap();
		
		/// Assert
		foreach (var item in result.GetView(0, 0, 99, 99))
		{
			item.ShouldNotBeNull();
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithoutThrivingOrOvergrownSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		foreach (var item in result.GetView(0, 0, 99, 99))
		{
			item.Fertility.ShouldNotBe(SoilFertility.Thriving);
			item.Fertility.ShouldNotBe(SoilFertility.Overgrown);
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithoutWetOrFloodedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory, Configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		foreach (var item in result.GetView(0, 0, 99, 99))
		{
			item.WaterLevel.ShouldNotBe(SoilWaterLevel.Wet);
			item.WaterLevel.ShouldNotBe(SoilWaterLevel.Flooded);
		}
	}
}
