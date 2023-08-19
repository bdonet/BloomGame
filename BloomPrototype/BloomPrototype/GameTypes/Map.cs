using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map()
    {
        Grid = new Soil[100, 100];

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                Grid[x, y] = new Soil
                {
                    Fertility = (SoilFertility)new Random().Next(1, 6)
                };
            }
        }

		var weedSoil = new Soil();
		var weed = new Weed(weedSoil);
		weedSoil.GrowingPlant = weed;
        weedSoil.Fertility = SoilFertility.Struggling;
		Grid[1, 0] = weedSoil;

		var tomatoSoil = new Soil();
		var tomato = new Tomato(tomatoSoil);
		tomatoSoil.GrowingPlant = tomato;
        tomatoSoil.Fertility = SoilFertility.Alive;
		Grid[4, 1] = tomatoSoil;

		var treeSoil = new Soil();
        var tree = new Tree(treeSoil);
        treeSoil.GrowingPlant = tree;
        treeSoil.Fertility = SoilFertility.Overgrown;
        Grid[2, 3] = treeSoil;

		var wheatSoil = new Soil();
		var wheat = new Wheat(wheatSoil);
		wheatSoil.GrowingPlant = wheat;
        wheatSoil.Fertility = SoilFertility.Thriving;
		Grid[1, 4] = wheatSoil;
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