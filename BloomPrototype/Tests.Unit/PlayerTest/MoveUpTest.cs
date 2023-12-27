using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class MoveUpTest
{
	[Fact]
	public void MoveUp_PlayerIsNotOnTopRow_IncrementsLocationY()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0);

		/// Act
		player.MoveUp();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 0));
	}

	[Fact]
	public void MoveUp_PlayerIsOnTopRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0);
		player.MoveUp();

		/// Act
		player.MoveUp();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 0));
	}
}
