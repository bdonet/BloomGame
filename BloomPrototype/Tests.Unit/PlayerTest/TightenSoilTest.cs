using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class TightenSoilTest
{
	[Fact]
	public void TightenSoil_PlayerHasAtLeastOneAction_DecrementsPlayerActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.TightenSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Theory]
	[InlineData(1, SoilRetention.Loose)]
	[InlineData(2, SoilRetention.Holding)]
	[InlineData(3, SoilRetention.Tight)]
	[InlineData(4, SoilRetention.Packed)]
	public void TightenSoil_PlayerHasAtLeastOneAction_PassesGivenLevelsToSoil(int levelsToIncrease,
																			SoilRetention expectedRetention)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		map.GetSoil(0, 0).Retention = SoilRetention.Dust;

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.TightenSoil(levelsToIncrease);

		/// Assert
		map.GetSoil(0, 0).Retention.ShouldBe(expectedRetention);
	}

	[Fact]
	public void TightenSoil_PlayerHasNoActions_DoesNotChangeActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.TightenSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Fact]
	public void TightenSoil_PlayerHasNoActions_DoesNotChangeSoilRetention()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		map.GetSoil(1, 1).Retention = SoilRetention.Dust;

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.TightenSoil(1);

		/// Assert
		map.GetSoil(1, 1).Retention.ShouldBe(SoilRetention.Dust);
	}
}
