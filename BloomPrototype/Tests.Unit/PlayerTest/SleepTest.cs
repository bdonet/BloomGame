using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class SleepTest
{
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(16)]
	[InlineData(int.MaxValue)]
	public void Sleep_GeneralCall_ResetsPlayerActionsToMax(int maxActionCount)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, 0, 0, maxActionCount);

		/// Act
		player.Sleep();

		/// Assert
		player.Actions.ShouldBe(maxActionCount);
	}
}
