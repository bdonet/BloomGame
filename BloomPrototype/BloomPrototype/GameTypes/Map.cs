using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map(Soil[,] soilGrid)
    {
		Grid = soilGrid;
	}

    public const int ViewSize = 7;

    public Soil[,] Grid;

    public Soil GetSoil(int x, int y) => Grid[x, y];
    
    public Soil[,] GetView(int startX, int startY, int endX, int endY)
	{
		var result = new Soil[endX - startX + 1, endY - startY + 1];

		for (int x = 0; x < endX - startX + 1; x++)
		{
			for (int y = 0; y < endY - startY + 1; y++)
			{
				result[x, y] = Grid[startX + x, startY + y];
			}
		}

		return result;
	}

    public Soil[,] GetView(int startX, int startY)
    {
		return GetView(startX, startY, startX + ViewSize - 1, startY + ViewSize - 1);
    }

	public Soil[,] GetViewWithTargetSoil(Soil targetSoil)
	{
		var targetLocation =  FindTargetSoil(targetSoil);

		var viewsX = targetLocation.Item1 / ViewSize;
		var viewsY = targetLocation.Item2 / ViewSize;

		var startX = viewsX * ViewSize;
		var startY = viewsY * ViewSize;
		return GetView(startX, startY);
    }

	private (int, int) FindTargetSoil(Soil targetSoil)
	{
		var worldSize = Grid.GetLength(0);

		for (int x = 0; x < worldSize; x++)
			for (int y = 0; y < worldSize; y++)
				if (GetSoil(x, y) == targetSoil)
					return (x, y);

		throw new KeyNotFoundException("Target Soil was not found in the Map");
	}

    public Soil[,] CopyGrid()
    {
        var worldSize = Grid.GetLength(0);

        var newGrid = new Soil[worldSize, worldSize];

		for (int x = 0; x < worldSize; x++)
		{
			for (int y = 0; y < worldSize; y++)
			{
                var originalSoil = Grid[x, y];

                newGrid[x, y] = new Soil
                {
                    Fertility = originalSoil.Fertility,
                    WaterLevel = originalSoil.WaterLevel,
                    Retention = originalSoil.Retention
                };
			}
		}

        return newGrid;
	}
}