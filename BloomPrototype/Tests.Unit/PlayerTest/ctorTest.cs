using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class ctorTest
{
	[Fact]
	public void ctor_GeneralCall_SetsPlayerLocationToSoilAtX1Y1()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var player = new Player(map);

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 1));
	}

	[Fact]
	public void ctor_GeneralCall_SetsPlayerActionsTo10()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var player = new Player(map);

		/// Assert
		player.Actions.ShouldBe(10);
	}
}
