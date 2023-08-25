using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes;

public class Map
{
    readonly ISoilFactory soilFactory;

    public Map(ISoilFactory soilFactory)
    {
        Grid = new Soil[100, 100];

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
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
}