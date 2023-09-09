using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using Telerik.JustMock;

namespace Tests.Unit.MapFactoryTest;
public class GenerateMap
{
	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(165)]
	[InlineData(1000)]
	public void GenerateMap_GridSizeIsWithinSizeBounds_ReturnsMapWithSoilArrayWithSquareOfGivenGridSize(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(expectedSize);

		/// Assert
		var view = result.GetView(0, 0, expectedSize - 1, expectedSize - 1);

		view.GetLength(0).ShouldBe(expectedSize);
		view.GetLength(1).ShouldBe(expectedSize);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-864)]
	[InlineData(int.MinValue)]
	public void GenerateMap_GridSizeIsNegative_ThrowsArgumentOutOfRangeException(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var exception = Record.Exception(() => factory.GenerateMap(expectedSize));

		/// Assert
		exception.ShouldBeOfType<ArgumentOutOfRangeException>();
		exception.Message.ShouldContain("small");
		exception.Message.ShouldContain(expectedSize.ToString());
	}

	[Theory]
	[InlineData(1001)]
	[InlineData(1002)]
	[InlineData(1000000)]
	[InlineData(int.MaxValue)]
	public void GenerateMap_GridSizeIsGreaterThanUpperBound_ThrowsArgumentOutOfRangeException(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var exception = Record.Exception(() => factory.GenerateMap(expectedSize));

		/// Assert
		exception.ShouldBeOfType<ArgumentOutOfRangeException>();
		exception.Message.ShouldContain("big");
		exception.Message.ShouldContain(expectedSize.ToString());
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

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(8);

		/// Assert
		Mock.Assert(() => soilFactory.GenerateSoil(), Occurs.Exactly(8 * 8));
		foreach (var s in result.GetView(0, 0, 7, 7))
		{
			s.Retention.ShouldBe(SoilRetention.Packed);
		}
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsAWeedWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(5);

		/// Assert
		var view = result.GetView(0, 0);

		view[1, 0].GrowingPlant.ShouldNotBeNull();
		view[1, 0].GrowingPlant.ShouldBeOfType<Weed>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsATomatoWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(5);

		/// Assert
		var view = result.GetView(0, 0);

		view[4, 1].GrowingPlant.ShouldNotBeNull();
		view[4, 1].GrowingPlant.ShouldBeOfType<Tomato>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsATreeWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(5);

		/// Assert
		var view = result.GetView(0, 0);

		view[2, 3].GrowingPlant.ShouldNotBeNull();
		view[2, 3].GrowingPlant.ShouldBeOfType<Tree>();
	}

	[Fact]
	public void GenerateMap_GridSizeIsWithinSizeBounds_MapGridContainsAWheatWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateMap(5);

		/// Assert
		var view = result.GetView(0, 0);

		view[1, 4].GrowingPlant.ShouldNotBeNull();
		view[1, 4].GrowingPlant.ShouldBeOfType<Wheat>();
	}
}
