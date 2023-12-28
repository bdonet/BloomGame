using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class FertilizeSoilTest
{
	[Fact]
	public void FertilizeSoil_PlayerHasAtLeastOneAction_DecrementsPlayerActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.FertilizeSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Theory]
	[InlineData(1, SoilFertility.Struggling)]
	[InlineData(2, SoilFertility.Alive)]
	[InlineData(3, SoilFertility.Thriving)]
	[InlineData(4, SoilFertility.Overgrown)]
	public void FertilizeSoil_PlayerHasAtLeastOneAction_PassesGivenLevelsToSoil(int levelsToIncrease,
																				SoilFertility expectedFertility)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);
		var coordinate = new MapCoordinate(0, 0, map);
		map.GetSoil(coordinate).Fertility = SoilFertility.Dead;

		var player = new Player(map, 0, 0, 1);

		/// Act
		player.FertilizeSoil(levelsToIncrease);

		/// Assert
		map.GetSoil(coordinate).Fertility.ShouldBe(expectedFertility);
	}

	[Fact]
	public void FertilizeSoil_PlayerHasNoActions_DoesNotChangeActionCount()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.FertilizeSoil(1);

		/// Assert
		player.Actions.ShouldBe(0);
	}

	[Fact]
	public void FertilizeSoil_PlayerHasNoActions_DoesNotChangeSoilFertility()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		var coordinate = new MapCoordinate(1, 1, map);
		map.GetSoil(coordinate).Fertility = SoilFertility.Dead;

		var player = new Player(map, 0, 0, 0);

		/// Act
		player.FertilizeSoil(1);

		/// Assert
		map.GetSoil(coordinate).Fertility.ShouldBe(SoilFertility.Dead);
	}
}
