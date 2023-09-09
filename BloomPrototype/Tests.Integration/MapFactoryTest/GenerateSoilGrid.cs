using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest;
public class GenerateSoilGrid : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void GenerateSoilGrid_GridSizeIsValid_ReturnsGridFullOfGeneratedSoil()
	{
		/// Arrange
		var factory = new MapFactory(SoilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(100);
		
		/// Assert
		foreach (var item in result)
		{
			item.ShouldNotBeNull();
		}
	}
}
