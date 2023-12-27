using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;

namespace Tests.Utilities;

public static class MapHelper
{
	public static Map SetupTestMap(int gridSize) { return SetupTestMap(gridSize, gridSize); }

	public static Map SetupTestMap(int xSize, int ySize)
	{
		var grid = new Soil[xSize, ySize];
		for (var i = 0; i < xSize; i++)
			for (var j = 0; j < ySize; j++)
				grid[i, j] = new Soil
							{
								Fertility = SoilFertility.Dead,
								WaterLevel = SoilWaterLevel.Parched,
								Retention = SoilRetention.Dust
							};

		return new Map(grid);
	}
}
