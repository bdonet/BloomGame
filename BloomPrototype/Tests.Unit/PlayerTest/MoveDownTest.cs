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

		var player = new Player(map, 1, 0, 0);

		var coordinate = new MapCoordinate(1, 1, map);

		/// Act
		player.MoveDown();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void MoveDown_PlayerIsOnBottomRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(3, 2);

		var player = new Player(map, 1, 1, 0);

		var coordinate = new MapCoordinate(1, 1, map);

		/// Act
		player.MoveDown();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}
}
