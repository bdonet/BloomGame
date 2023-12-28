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
		var coordinate = new MapCoordinate(0, 1, map);

		var player = new Player(map, 1, 1, 0);

		/// Act
		player.MoveLeft();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void MoveLeft_PlayerIsOnLeftmostRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		var coordinate = new MapCoordinate(0, 1, map);

		var player = new Player(map, 0, 1, 0);

		/// Act
		player.MoveLeft();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}
}
