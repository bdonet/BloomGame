using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;

namespace Tests.Utilities;

public static class MapHelper
{
	public static Map SetupTestMap(int gridSize)
	{
		var grid = new Soil[gridSize, gridSize];
		for(var i = 0; i < gridSize; i++)
			for(var j = 0; j < gridSize; j++)
				grid[i, j] = new Soil();

		return new Map(grid);
	}
}
