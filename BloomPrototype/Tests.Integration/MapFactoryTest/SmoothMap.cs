using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest;

public class SmoothMap : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void SmoothMap_GeneralCall_DoesNotThrowException()
	{
		/// Arrange
		var random = new RandomNumberGenerator();
		var mapFactory = new MapFactory(SoilFactory, Configuration, random);

		var map = mapFactory.GenerateMap();

		/// Act
		var exception = Record.Exception(() => mapFactory.SmoothMap(map));

		/// Assert
		exception.ShouldBeNull();
	}
}
