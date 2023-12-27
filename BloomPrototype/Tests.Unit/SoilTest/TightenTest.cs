using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Unit.SoilTest;

public class TightenTest
{
	[Theory]
	[InlineData(1, SoilRetention.Loose)]
	[InlineData(2, SoilRetention.Holding)]
	[InlineData(3, SoilRetention.Tight)]
	[InlineData(4, SoilRetention.Packed)]
	public void Tighten_TighteningMinimumSoil_IncreasesSoilRetentionByGivenLevels(int levelsToIncrease,
																				SoilRetention expectedRetention)
	{
		/// Arrange
		var soil = new Soil { Retention = SoilRetention.Dust };

		/// Act
		soil.Tighten(levelsToIncrease);

		/// Assert
		soil.Retention.ShouldBe(expectedRetention);
	}

	[Theory]
	[InlineData(SoilRetention.Dust, SoilRetention.Loose)]
	[InlineData(SoilRetention.Loose, SoilRetention.Holding)]
	[InlineData(SoilRetention.Holding, SoilRetention.Tight)]
	[InlineData(SoilRetention.Tight, SoilRetention.Packed)]
	public void Tighten_TighteningNonMaxedSoilByOneLevel_IncreasesSoilRetentionByOneLevel(SoilRetention originalRetention,
																						SoilRetention expectedRetention)
	{
		/// Arrange
		var soil = new Soil { Retention = originalRetention };

		/// Act
		soil.Tighten(1);

		/// Assert
		soil.Retention.ShouldBe(expectedRetention);
	}

	[Theory]
	[InlineData(1, SoilRetention.Packed)]
	[InlineData(2, SoilRetention.Tight)]
	[InlineData(3, SoilRetention.Holding)]
	[InlineData(4, SoilRetention.Loose)]
	[InlineData(5, SoilRetention.Dust)]
	public void Tighten_TighteningSoilBeyondMaxRetention_SetsSoilRetentionToMax(int levelsToIncrease,
																				SoilRetention originalRetention)
	{
		/// Arrange
		var soil = new Soil { Retention = originalRetention };

		/// Act
		soil.Tighten(levelsToIncrease);

		/// Assert
		soil.Retention.ShouldBe(SoilRetention.Packed);
	}
}
