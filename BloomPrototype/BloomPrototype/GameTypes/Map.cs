using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map()
    {
        Grid = new Soil[100, 100];
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