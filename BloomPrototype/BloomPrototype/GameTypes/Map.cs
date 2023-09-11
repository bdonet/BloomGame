using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map(Soil[,] soilGrid)
    {
		Grid = soilGrid;
	}

    public const int ViewSize = 5;
    public const int WorldSize = 100;

    private Soil[,] Grid;

    public Soil GetSoil(int x, int y) => Grid[x, y];
    
    public Soil[,] GetView(int startX, int startY, int endX, int endY)
	{
		var result = new Soil[endX - startX + 1, endY - startY + 1];

		for (int x = 0; x < endX + 1; x++)
		{
			for (int y = 0; y < endY + 1; y++)
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

    public void SmoothMap(ISoilFactory factory, int contextRadius, double extremesWeight)
    {
        var originalGrid = CopyGrid();

		for (int x = 0; x < WorldSize; x++)
		{
			for (int y = 0; y < WorldSize; y++)
			{
                // Add surrounding 4 pieces of soil to the context if they are available
				var contextSoils = GetContextSoils(x, y, originalGrid, contextRadius);
				factory.SmoothSoil(Grid[x, y], contextSoils, extremesWeight);
			}
		}
	}

    private List<Soil> GetContextSoils(int x, int y, Soil[,] originalGrid, int contextRadius)
    {
		// Add surrounding pieces of soil to the context if they are available
		var indexes = new List<(int, int)>();
		for (int i = -contextRadius; i < contextRadius + 1; i++)
		{
			var yOffset = contextRadius - Math.Abs(i);

			var currentX = x + i;
			var yBottom = y - yOffset;
			var yTop = y + yOffset;

			for (int j = yBottom; j < yTop + 1; j++)
			{
				indexes.Add((currentX, j));
			}
		}

		indexes = indexes.Distinct().ToList();
		indexes = indexes.Where(i => i.Item1 >= 0
									 && i.Item1 < WorldSize
									 && i.Item2 >= 0
									 && i.Item2 < WorldSize
									 && !(i.Item1 == x && i.Item2 == y))
						 .ToList();

		var contextSoils = new List<Soil>();
		foreach (var i in indexes)
		{
			contextSoils.Add(originalGrid[i.Item1, i.Item2]);
		}

		return contextSoils;
	}

    private Soil[,] CopyGrid()
    {
        var newGrid = new Soil[WorldSize, WorldSize];

		for (int x = 0; x < WorldSize; x++)
		{
			for (int y = 0; y < WorldSize; y++)
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