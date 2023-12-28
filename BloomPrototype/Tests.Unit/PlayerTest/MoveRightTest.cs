using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class MoveRightTest
{
	[Fact]
	public void MoveRight_PlayerIsNotOnRightmostRow_IncrementsLocationX()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);
		var coordinate = new MapCoordinate(1, 1, map);

		var player = new Player(map, 0, 1, 0);

		/// Act
		player.MoveRight();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}

	[Fact]
	public void MoveRight_PlayerIsOnRightmostRow_DoesNotChangeLocation()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2, 3);
		var coordinate = new MapCoordinate(1, 1, map);

		var player = new Player(map, 1, 1, 0);

		/// Act
		player.MoveRight();

		/// Assert
		player.Location.ShouldBe(map.GetSoil(coordinate));
	}
}
