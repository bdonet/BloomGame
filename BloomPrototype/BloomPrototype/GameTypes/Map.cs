using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes;

public class Map
{
    readonly ISoilFactory soilFactory;

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
				var contextSoils = new List<Soil>();
				if (x > 0)
					contextSoils.Add(originalGrid[x - 1, y]);
				if (x < WorldSize - 1)
					contextSoils.Add(originalGrid[x + 1, y]);
				if (y > 0)
					contextSoils.Add(originalGrid[x, y - 1]);
				if (y < WorldSize - 1)
					contextSoils.Add(originalGrid[x, y + 1]);

				factory.SmoothSoil(Grid[x, y], contextSoils);

			}
		}
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