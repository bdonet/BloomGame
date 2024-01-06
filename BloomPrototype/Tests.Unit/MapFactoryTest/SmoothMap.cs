using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Telerik.JustMock;
using Tests.Utilities;

namespace Tests.Unit.MapFactoryTest;

public class SmoothMap
{
	[Fact]
	public void SmoothMap_GeneralCall_SmoothsSoilUsingAvailableSurroundingSoilsAsContextSoils()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["WorldSize"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var mapFactory = new MapFactory(soilFactory, configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		mapFactory.SmoothMap(MapHelper.SetupTestMap(5));

		/// Assert
		// Should not have some context soils when near grid edge
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 5), 2, 20),
					Occurs.Exactly(4));
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 7), 2, 20),
					Occurs.Exactly(8));
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 8), 2, 20),
					Occurs.Exactly(4));
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 10), 2, 20),
					Occurs.Exactly(4));
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 11), 2, 20),
					Occurs.Exactly(4));
	}

	[Fact]
	public void SmoothMap_GeneralCall_SmoothsSoilUsingSoilFactoryForEachSoilInMap()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["WorldSize"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var mapFactory = new MapFactory(soilFactory, configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		mapFactory.SmoothMap(MapHelper.SetupTestMap(5));

		/// Assert
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.IsAny<List<Soil>>(), 2, 20),
					Occurs.Exactly(5 * 5));
	}

	[Fact]
	public void SmoothMap_GeneralCall_SmoothsSoilUsingSurroundingSoilsAsContextSoils()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["WorldSize"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var mapFactory = new MapFactory(soilFactory, configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		mapFactory.SmoothMap(MapHelper.SetupTestMap(5));

		/// Assert
		// Should have (r + 1)^2 + r^2 - 1 context soils in center where r is the context radius
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.Matches<List<Soil>>(c => c.Count == 12), 2, 20),
					Occurs.AtLeastOnce());
	}

	[Fact]
	public void SmoothMap_IsInitialGeneration_SmoothsSoilWith0OffsetChance()
	{
		/// Arrange
		int? actualOffsetChance = null;

		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.IsAny<List<Soil>>(), Arg.AnyDouble, Arg.AnyInt))
			.DoInstead<Soil, List<Soil>, double, int>((_, _, _, offsetChance) => actualOffsetChance = offsetChance);

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["WorldSize"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var mapFactory = new MapFactory(soilFactory, configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		mapFactory.SmoothMap(MapHelper.SetupTestMap(5), true);

		/// Assert
		actualOffsetChance.ShouldBe(0);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(5)]
	[InlineData(10)]
	[InlineData(20)]
	[InlineData(50)]
	public void SmoothMap_IsNotInitialGeneration_SmoothsSoilWithConfiguredOffsetChance(int expectedOffsetChance)
	{
		/// Arrange
		int? actualOffsetChance = null;

		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.IsAny<List<Soil>>(), Arg.AnyDouble, Arg.AnyInt))
			.DoInstead<Soil, List<Soil>, double, int>((_, _, _, offsetChance) => actualOffsetChance = offsetChance);

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["WorldSize"]).Returns(0.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(expectedOffsetChance.ToString());

		var mapFactory = new MapFactory(soilFactory, configuration, Mock.Create<IRandomNumberGenerator>());

		/// Act
		mapFactory.SmoothMap(MapHelper.SetupTestMap(5), false);

		/// Assert
		actualOffsetChance.ShouldBe(expectedOffsetChance);
	}
}
