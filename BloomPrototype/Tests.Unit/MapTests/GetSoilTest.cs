using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapTests;

public class GetSoilTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void GetSoil_GeneralCall_ReturnsSoilAtGivenCoordinates(int x, int y)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var result = map.GetSoil(x, y);

		/// Assert
		result.ShouldBe(map.Grid[x, y]);
	}
}
