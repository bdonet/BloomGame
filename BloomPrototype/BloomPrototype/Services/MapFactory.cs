using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.Services;

public class MapFactory
{
	private const int LowerGridSizeBound = 1;
	private const int UpperGridSizeBound = 1000;

	private readonly ISoilFactory _soilFactory;

	public MapFactory(ISoilFactory soilFactory)
	{
		_soilFactory = soilFactory;
	}

	public Soil[,] GenerateSoilGrid(int gridSize)
	{
		if (gridSize < LowerGridSizeBound)
			throw new ArgumentOutOfRangeException($"{gridSize} is too small. The smallest possible grid has size of 1");

		if (gridSize > UpperGridSizeBound)
			throw new ArgumentOutOfRangeException($"{gridSize} is too big. The largest possible grid has size of 500000");

		var result = new Soil[gridSize, gridSize];
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				result[x, y] = _soilFactory.GenerateSoil();
			}
		}

		if (gridSize >= 5)
		{
			var weed = new Weed(result[1, 0]);
			result[1, 0].GrowingPlant = weed;

			var tomato = new Tomato(result[4, 1]);
			result[4, 1].GrowingPlant = tomato;

			var tree = new Tree(result[2, 3]);
			result[2, 3].GrowingPlant = tree;

			var wheat = new Wheat(result[1, 4]);
			result[1, 4].GrowingPlant = wheat;
		}
		return result;
	}
}
