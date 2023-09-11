﻿using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.Services;

public class MapFactory
{
	private readonly int LowerGridSizeBound;
	private readonly int UpperGridSizeBound;
	private readonly int ContextRadius;
	private readonly double ExtremesWeight;
	private readonly int WorldSize;

	private readonly ISoilFactory _soilFactory;

	public MapFactory(ISoilFactory soilFactory, IConfiguration configuration)
	{
		_soilFactory = soilFactory;

		LowerGridSizeBound = Convert.ToInt32(configuration["LowerGridSizeBound"]);
		UpperGridSizeBound = Convert.ToInt32(configuration["UpperGridSizeBound"]);
		ContextRadius = Convert.ToInt32(configuration["ContextRadius"]);
		ExtremesWeight = Convert.ToInt32(configuration["ExtremesWeight"]);
		WorldSize = Convert.ToInt32(configuration["WorldSize"]);
	}

	public Map GenerateMap()
	{
		var map = new Map(GenerateSoilGrid(WorldSize));
        SmoothMap(map);
        return map;
	}

	private Soil[,] GenerateSoilGrid(int gridSize)
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

    public void SmoothMap(Map map)
    {
        var originalGrid = map.CopyGrid();

        for (int x = 0; x < originalGrid.GetLength(0); x++)
        {
            for (int y = 0; y < originalGrid.GetLength(0); y++)
            {
                // Add surrounding pieces of soil to the context if they are available
                var contextSoils = GetContextSoils(x, y, originalGrid);
                _soilFactory.SmoothSoil(map.Grid[x, y], contextSoils, ExtremesWeight);
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
                                     && i.Item1 < originalGrid.GetLength(0)
                                     && i.Item2 >= 0
                                     && i.Item2 < originalGrid.GetLength(0)
                                     && !(i.Item1 == x && i.Item2 == y))
                         .ToList();

        var contextSoils = new List<Soil>();
        foreach (var i in indexes)
        {
            contextSoils.Add(originalGrid[i.Item1, i.Item2]);
        }

        return contextSoils;
    }
}