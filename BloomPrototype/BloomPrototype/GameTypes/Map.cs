using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map(ISoilFactory soilFactory)
    {
        Grid = new Soil[WorldSize, WorldSize];

        for (int x = 0; x < WorldSize; x++)
        {
            for (int y = 0; y < WorldSize; y++)
            {
                Grid[x, y] = soilFactory.GenerateSoil();
            }
        }

		var weed = new Weed(Grid[1, 0]);
		Grid[1, 0].GrowingPlant = weed;

		var tomato = new Tomato(Grid[4, 1]);
		Grid[4, 1].GrowingPlant = tomato;

        var tree = new Tree(Grid[2, 3]);
		Grid[2, 3].GrowingPlant = tree;

		var wheat = new Wheat(Grid[1, 4]);
		Grid[1, 4].GrowingPlant = wheat;
	}

    public const int ViewSize = 5;
    public const int WorldSize = 100;
	public const int ContextRadius = 2;

    private Soil[,] Grid;

    public Soil GetSoil(int x, int y) => Grid[x, y];
    
    public Soil[,] GetView(int startX, int startY)
    {
        var result = new Soil[ViewSize, ViewSize];

        for (int x = 0; x < ViewSize; x++)
        {
            for (int y = 0; y < ViewSize; y++)
            {
                result[x, y] = Grid[startX + x, startY + y];
            }
        }

        return result;
    }

    public void SmoothMap(ISoilFactory factory)
    {
        var originalGrid = CopyGrid();

		for (int x = 0; x < WorldSize; x++)
		{
			for (int y = 0; y < WorldSize; y++)
			{
                // Add surrounding 4 pieces of soil to the context if they are available
				var contextSoils = GetContextSoils(x, y, originalGrid);
				factory.SmoothSoil(Grid[x, y], contextSoils);
			}
		}
	}

    private List<Soil> GetContextSoils(int x, int y, Soil[,] originalGrid)
    {
		// Add surrounding pieces of soil to the context if they are available
		var indexes = new List<(int, int)>();
		for (int i = -ContextRadius; i < ContextRadius + 1; i++)
		{
			var yOffset = ContextRadius - Math.Abs(i);

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