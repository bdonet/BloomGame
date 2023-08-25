using BloomPrototype.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit.RandomNumberGeneratorTest;
public class GenerateIntTest
{
	[Theory]
	[InlineData(0, 1)]
	[InlineData(1, 5)]
	[InlineData(-8, 61)]
	[InlineData(int.MinValue, int.MaxValue)]
	public void GenerateInt_GeneralCall_ReturnsIntegerBetweenOrIncludingBounds(int low, int high)
	{
		// Run 10 times to be a little more sure with the randomness
		for (int i = 0; i < 10; i++)
		{
			/// Arrange
			var generator = new RandomNumberGenerator();

			/// Act
			var result = generator.GenerateInt(low, high);

			/// Assert
			result.ShouldBeGreaterThanOrEqualTo(low);
			result.ShouldBeLessThanOrEqualTo(high);
		}
	}
}
