using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class ctorTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void ctor_GeneralCall_SetsPlayerLocationToGivenLocationXAndLocationY(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var expectedCoordinate = new MapCoordinate(expectedX, expectedY, map);

		/// Act
		var player = new Player(map, expectedX, expectedY, 0);

		/// Assert
		player.Location.ShouldBe(map.GetSoil(expectedCoordinate));
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
		var player = new Player(map, 0, 0, count);

		/// Assert
		player.Actions.ShouldBe(count);
	}
}
