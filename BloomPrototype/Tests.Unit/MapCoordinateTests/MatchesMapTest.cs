using BloomPrototype.GameTypes;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapCoordinateTests;

public class MatchesMapTest
{
	[Fact]
	public void MatchesMap_GivenMapMatchesStoredMap_ReturnsTrue()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var coordinate = new MapCoordinate(0, 0, map);

		/// Act
		var result = coordinate.MatchesMap(map);

		/// Assert
		result.ShouldBeTrue();
	}

	[Fact]
	public void MatchesMap_GivenMapDoesNotMatchStoredMap_ReturnsFalse()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		var coordinate = new MapCoordinate(0, 0, MapHelper.SetupTestMap(1));

		/// Act
		var result = coordinate.MatchesMap(map);

		/// Assert
		result.ShouldBeFalse();
	}
}
