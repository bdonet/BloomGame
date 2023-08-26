using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Integration.SoilFactoryTest;
public class GenerateSoilTest : BaseSoilFactoryIntegrationTest
{
	[Fact]
	public void GenerateSoil_GeneralCall_SoilHasValidFertility()
	{
		/// Arrange
		var factory = new SoilFactory(Generator);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		((int)result.Fertility).ShouldBeInRange((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);
	}

	[Fact]
	public void GenerateSoil_GeneralCall_SoilHasValidWaterLevel()
	{
		/// Arrange
		var factory = new SoilFactory(Generator);

		/// Act
		var result = factory.GenerateSoil();

		/// Assert
		((int)result.WaterLevel).ShouldBeInRange((int)SoilWaterLevel.Parched, (int)SoilWaterLevel.Flooded);
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
