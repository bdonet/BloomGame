using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;

namespace Tests.Unit.MapFactoryTest
{
	public class SmoothMap
	{
		[Fact]
		public void SmoothMap_GeneralCall_SmoothsSoilUsingSoilFactoryForEachSoilInMap()
		{
			/// Arrange
			var soilFactory = Mock.Create<ISoilFactory>();

			var configuration = Mock.Create<IConfiguration>();
			Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
			Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());

			var mapFactory = new MapFactory(soilFactory, configuration);

			/// Act
			mapFactory.SmoothMap(new Map(new Soil[5, 5]));

			/// Assert
			Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.IsAny<List<Soil>>(), 2));
		}
	}
}
