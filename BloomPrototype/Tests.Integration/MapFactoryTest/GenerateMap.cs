using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest;
public class GenerateMap : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void GenerateMap_GridSizeIsValid_ReturnsMapWithGridFullOfGeneratedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory);

		/// Act
		var result = factory.GenerateMap(100);
		
		/// Assert
		foreach (var item in result.GetView(0, 0, 99, 99))
		{
			item.ShouldNotBeNull();
		}
	}
}
