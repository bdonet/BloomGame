using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.MapTests;

public class SizeTest
{
	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(16)]
	[InlineData(100)]
	public void Size_GeneralCall_ReturnsSizeOfOneMapDimension(int expectedSize)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(expectedSize);

		/// Act
		var result = map.Size;

		/// Assert
		result.ShouldBe(expectedSize);
	}
}
