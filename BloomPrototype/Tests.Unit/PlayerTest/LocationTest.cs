using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlayerTest;

public class LocationTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void Location_GeneralCall_ReturnsSoilFromMapAtPlayerLocation(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		var player = new Player(map, expectedX, expectedY, 0);

		/// Act
		var result = player.Location;

		/// Assert
		result.ShouldBe(map.GetSoil(expectedX, expectedY));
	}
}
