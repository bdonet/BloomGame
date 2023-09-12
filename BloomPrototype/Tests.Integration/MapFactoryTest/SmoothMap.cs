using BloomPrototype.Services;
using Shouldly;

namespace Tests.Integration.MapFactoryTest
{
	public class SmoothMap : BaseMapFactoryIntegrationTest
	{
		[Fact]
		public void SmoothMap_GeneralCall_DoesNotThrowException()
		{
			/// Arrange
			var mapFactory = new MapFactory(SoilFactory, Configuration);

			var map = mapFactory.GenerateMap();

			/// Act
			var exception = Record.Exception(() => mapFactory.SmoothMap(map));

			/// Assert
			exception.ShouldBeNull();
		}
	}
}
