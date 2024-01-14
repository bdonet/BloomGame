using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest;

public class AgeMap : BaseMapFactoryIntegrationTest
{
	[Fact]
	public void AgeMap_GeneralCall_DoesNotThrowException()
	{
		/// Arrange
		var random = new RandomNumberGenerator();
		var mapFactory = new MapFactory(SoilFactory, Configuration, random);

		var map = mapFactory.GenerateMap();

		/// Act
		var exception = Record.Exception(() => mapFactory.AgeMap(map));

		/// Assert
		exception.ShouldBeNull();
	}
}
