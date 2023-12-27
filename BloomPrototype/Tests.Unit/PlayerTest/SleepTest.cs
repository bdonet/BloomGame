using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class SleepTest
{
	[Fact]
	public void Sleep_GeneralCall_ResetsPlayerActionsToMax()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0);

		/// Act
		player.Sleep();

		/// Assert
		player.Actions.ShouldBe(10);
	}
}
