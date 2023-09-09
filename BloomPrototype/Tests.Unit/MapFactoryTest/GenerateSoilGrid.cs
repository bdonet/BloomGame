using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using Telerik.JustMock;

namespace Tests.Unit.MapFactoryTest;
public class GenerateSoilGrid
{
	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(165)]
	[InlineData(1000)]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_ReturnsSoilArrayWithSquareOfGivenGridSize(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(expectedSize);

		/// Assert
		result.GetLength(0).ShouldBe(expectedSize);
		result.GetLength(1).ShouldBe(expectedSize);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-864)]
	[InlineData(int.MinValue)]
	public void GenerateSoilGrid_GridSizeIsNegative_ThrowsArgumentOutOfRangeException(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var exception = Record.Exception(() => factory.GenerateSoilGrid(expectedSize));

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
	public void GenerateSoilGrid_GridSizeIsGreaterThanUpperBound_ThrowsArgumentOutOfRangeException(int expectedSize)
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var exception = Record.Exception(() => factory.GenerateSoilGrid(expectedSize));

		/// Assert
		exception.ShouldBeOfType<ArgumentOutOfRangeException>();
		exception.Message.ShouldContain("big");
		exception.Message.ShouldContain(expectedSize.ToString());
	}

	[Fact]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_GeneratesSoilForEachSpotInArrayUsingSoilGenerator()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil
		{
			Retention = SoilRetention.Packed
		});

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(8);

		/// Assert
		Mock.Assert(() => soilFactory.GenerateSoil(), Occurs.Exactly(8 * 8));
		foreach (var s in result)
		{
			s.Retention.ShouldBe(SoilRetention.Packed);
		}
	}

	[Fact]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_GridContainsAWeedWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(5);

		/// Assert
		result[1, 0].GrowingPlant.ShouldNotBeNull();
		result[1, 0].GrowingPlant.ShouldBeOfType<Weed>();
	}

	[Fact]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_GridContainsATomatoWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(5);

		/// Assert
		result[4, 1].GrowingPlant.ShouldNotBeNull();
		result[4, 1].GrowingPlant.ShouldBeOfType<Tomato>();
	}

	[Fact]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_GridContainsATreeWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(5);

		/// Assert
		result[2, 3].GrowingPlant.ShouldNotBeNull();
		result[2, 3].GrowingPlant.ShouldBeOfType<Tree>();
	}

	[Fact]
	public void GenerateSoilGrid_GridSizeIsWithinSizeBounds_GridContainsAWheatWithinFirst5By5()
	{
		/// Arrange
		var soilFactory = Mock.Create<ISoilFactory>();
		Mock.Arrange(() => soilFactory.GenerateSoil()).Returns(() => new Soil());

		var factory = new MapFactory(soilFactory);

		/// Act
		var result = factory.GenerateSoilGrid(5);

		/// Assert
		result[1, 4].GrowingPlant.ShouldNotBeNull();
		result[1, 4].GrowingPlant.ShouldBeOfType<Wheat>();
	}
}
