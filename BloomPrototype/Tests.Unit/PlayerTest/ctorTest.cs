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
		var player = new Player(map, 0);

		/// Assert
		player.Location.ShouldBe(map.GetSoil(1, 1));
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(35)]
	[InlineData(int.MaxValue)]
	public void ctor_GeneralCall_SetsPlayerActionsToGivenCount(int count)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var player = new Player(map, count);

		/// Assert
		player.Actions.ShouldBe(count);
	}
}
