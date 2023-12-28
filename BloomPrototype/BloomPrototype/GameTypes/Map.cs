using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Map
{
    public Map(Soil[,] soilGrid)
    {
		Grid = soilGrid;
	}

    public const int ViewSize = 7;

	/// <summary>
	/// Returns the length of one side of the square map
	/// </summary>
	public int Size => Grid.GetUpperBound(0) + 1;

    public Soil[,] Grid;

    private Soil GetSoil(int x, int y) => Grid[x, y];
    public Soil GetSoil(MapCoordinate coordinate)
	{
		if (!coordinate.MatchesMap(this))
			throw new InvalidOperationException($"Given {nameof(MapCoordinate)} was created for a different {nameof(Map)}");

		return GetSoil(coordinate.X, coordinate.Y);
	}
    
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

	/// <summary>
	/// Returns the portion of the map bounded by the given coordinates, inclusively
	/// </summary>
	public Soil[,] GetView(MapCoordinate startCoordinate, MapCoordinate endCoordinate)
	{
		if (!startCoordinate.MatchesMap(this) || !endCoordinate.MatchesMap(this))
			throw new InvalidOperationException($"Given {nameof(MapCoordinate)} was created for a different {nameof(Map)}");

		var result = new Soil[endCoordinate.X - startCoordinate.X + 1, endCoordinate.Y - startCoordinate.Y + 1];

		for (int x = 0; x < endCoordinate.X - startCoordinate.X + 1; x++)
		{
			for (int y = 0; y < endCoordinate.Y - startCoordinate.Y + 1; y++)
			{
				result[x, y] = Grid[startCoordinate.X + x, startCoordinate.Y + y];
			}
		}

		return result;
	}

    public Soil[,] GetView(int startX, int startY)
    {
		return GetView(startX, startY, startX + ViewSize - 1, startY + ViewSize - 1);
    }

	public Soil[,] GetView(MapCoordinate startCoordinate)
	{
		return GetView(startCoordinate, new MapCoordinate(startCoordinate.X + ViewSize - 1, startCoordinate.Y + ViewSize - 1, this));
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