using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Integration.SoilFactoryTest;
public class GenerateSoil : BaseSoilFactoryIntegrationTest
{
	[Fact]
	public void GenerateSoil_GeneralCall_SoilHasDesertLikeFertility()
	{
		/// Arrange
		var factory = new SoilFactory(Generator);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		((int)result.Fertility).ShouldBeInRange((int)SoilFertility.Dead, (int)SoilFertility.Struggling);
	}

	[Fact]
	public void GenerateSoil_GeneralCall_SoilHasDesertLikeWaterLevel()
	{
		/// Arrange
		var factory = new SoilFactory(Generator);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		((int)result.WaterLevel).ShouldBeInRange((int)SoilWaterLevel.Parched, (int)SoilWaterLevel.Dry);
	}

	[Fact]
	public void GenerateSoil_GeneralCall_SoilHasValidRetention()
	{
		/// Arrange
		var factory = new SoilFactory(Generator);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		((int)result.Retention).ShouldBeInRange((int)SoilRetention.Dust, (int)SoilRetention.Packed);
	}
}
