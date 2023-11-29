using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit.PlayerTest;
public class ctor
{
	[Fact]
	public void ctor_GeneralCall_SetsPlayerLocationToSoilAtX1Y1()
	{
		/// Arrange
		var soilMap = new Soil[2, 2];
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				soilMap[i, j] = new Soil();
			}
		}

		var map = new Map(soilMap);

		/// Act
		var player = new Player(map);

		/// Assert
		player.Location.ShouldBe(soilMap[1, 1]);
	}
}
