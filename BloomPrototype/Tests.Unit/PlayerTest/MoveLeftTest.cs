using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class MoveLeftTest
{
	[Fact]
	public void MoveLeft_PlayerIsNotOnLeftmostRow_DecrementsLocationX()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0);

		/// Act
		player.MoveLeft();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(0, 1));
	}

	[Fact]
	public void MoveLeft_PlayerIsOnLeftmostRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0);
		player.MoveLeft();

		/// Act
		player.MoveLeft();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(0, 1));
	}
}
