using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class MoveDownTest
{
	[Fact]
	public void MoveDown_PlayerIsNotOnBottomRow_DecrementsLocationY()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map);
		player.MoveUp();

		/// Act
		player.MoveDown();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 1));
	}

	[Fact]
	public void MoveDown_PlayerIsOnBottomRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(3, 2);

		var player = new Player(map);

		/// Act
		player.MoveDown();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 1));
	}
}
