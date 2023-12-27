using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Telerik.JustMock;

namespace Tests.Unit.MapFactoryTest;

public class GenerateMap
{
	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(165)]
	public void GenerateMap_GridSizeIsWithinSizeBounds_ReturnsMapWithSoilArrayWithSquareOfGivenGridSize(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(expectedSize.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(165.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		var view = result.GetView(0, 0, expectedSize - 1, expectedSize - 1);

		view.GetLength(0).ShouldBe(expectedSize);
		view.GetLength(1).ShouldBe(expectedSize);
	}

	[Fact]
	public void GenerateMap_GridSizeIsBelowLowerBound_ThrowsArgumentOutOfRangeException()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(18.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(20.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(165.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var exception = Record.Exception(() => factory.GenerateMap());

		/// Assert
		exception.ShouldBeOfType<ArgumentOutOfRangeException>();
		exception.Message.ShouldContain("small");
		exception.Message.ShouldContain("18");
	}

	[Fact]
	public void GenerateMap_GridSizeIsGreaterThanUpperBound_ThrowsArgumentOutOfRangeException()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(180.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(20.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(165.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var exception = Record.Exception(() => factory.GenerateMap());

		/// Assert
		exception.ShouldBeOfType<ArgumentOutOfRangeException>();
		exception.Message.ShouldContain("big");
		exception.Message.ShouldContain("180");
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_GeneratesSoilForEachSpotInMapArrayUsingSoilGenerator()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil
		{
			Retention = SoilRetention.Packed
		});

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(8.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		Mock.Assert(() => soilFactory.GenerateSoil(), Occurs.Exactly(8 * 8));
		foreach (var s in result.GetView(0, 0, 7, 7))
		{
			s.Retention.ShouldBe(SoilRetention.Packed);
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsAWeedWithinFirst7By7()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(7.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		var view = result.GetView(0, 0);

		view[1, 0].GrowingPlant.ShouldNotBeNull();
		view[1, 0].GrowingPlant.ShouldBeOfType<Weed>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsATomatoWithinFirst7By7()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(7.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		var view = result.GetView(0, 0);

		view[4, 1].GrowingPlant.ShouldNotBeNull();
		view[4, 1].GrowingPlant.ShouldBeOfType<Tomato>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsATreeWithinFirst7By7()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(7.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		var view = result.GetView(0, 0);

		view[2, 3].GrowingPlant.ShouldNotBeNull();
		view[2, 3].GrowingPlant.ShouldBeOfType<Tree>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsAWheatWithinFirst7By7()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(7.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		var view = result.GetView(0, 0, 6, 6);

		view[6, 4].GrowingPlant.ShouldNotBeNull();
		view[6, 4].GrowingPlant.ShouldBeOfType<Wheat>();
	}

	[Fact]
	public void GenerateMap_GeneralCall_SmoothsSoilAsInitialGeneration()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => configuration["WorldSize"]).Returns(7.ToString());
		Mock.Arrange(() => configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["UpperGridSizeBound"]).Returns(100.ToString());
		Mock.Arrange(() => configuration["ContextRadius"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["ExtremesWeight"]).Returns(1.ToString());
		Mock.Arrange(() => configuration["SoilOffsetPercentChance"]).Returns(20.ToString());

		var factory = new MapFactory(soilFactory, configuration);

		/// Act
		var result = factory.GenerateMap();

		/// Assert
		Mock.Assert(() => soilFactory.SmoothSoil(Arg.IsAny<Soil>(), Arg.IsAny<List<Soil>>(), Arg.AnyDouble, 0), Occurs.AtLeastOnce());
	}
}
