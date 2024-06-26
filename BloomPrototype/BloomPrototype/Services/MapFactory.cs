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
	private readonly int SoilOffsetPercentChance;

	private readonly ISoilFactory _soilFactory;
	readonly IRandomNumberGenerator random;

	public MapFactory(ISoilFactory soilFactory, IConfiguration configuration, IRandomNumberGenerator random)
	{
		this.random = random;
		_soilFactory = soilFactory;

		LowerGridSizeBound = Convert.ToInt32(configuration["LowerGridSizeBound"]);
		UpperGridSizeBound = Convert.ToInt32(configuration["UpperGridSizeBound"]);
		ContextRadius = Convert.ToInt32(configuration["ContextRadius"]);
		ExtremesWeight = Convert.ToDouble(configuration["ExtremesWeight"]);
		WorldSize = Convert.ToInt32(configuration["WorldSize"]);
		SoilOffsetPercentChance = Convert.ToInt32(configuration["SoilOffsetPercentChance"]);
	}

	public Map GenerateMap()
	{
		var map = new Map(GenerateSoilGrid(WorldSize));
        AgeMap(map, true);

		if (WorldSize >= 7)
		{
			var cactus = new Cactus(map, 1, 0, PlantMaturity.Young, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(1, 0, map)).GrowingPlant = cactus;

			var cactusSprout = new Cactus(map, 1, 1, PlantMaturity.Sprout, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(1, 1, map)).GrowingPlant = cactusSprout;

			var tomato = new Tomato(map, 4, 1, PlantMaturity.Young, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(4, 1, map)).GrowingPlant = tomato;

			var tomatoSprout = new Tomato(map, 4, 2, PlantMaturity.Sprout, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(4, 2, map)).GrowingPlant = tomatoSprout;

			var tree = new Tree(map, 2, 3, PlantMaturity.Young, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(2, 3, map)).GrowingPlant = tree;

			var treeSprout = new Tree(map, 1, 3, PlantMaturity.Sprout, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(1, 3, map)).GrowingPlant = treeSprout;

			var wheat = new Wheat(map, 6, 4, PlantMaturity.Young, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(6, 4, map)).GrowingPlant = wheat;

			var wheatSprout = new Wheat(map, 6, 5, PlantMaturity.Sprout, PlantHealth.Stable, 0, random);
			map.GetSoil(new MapCoordinate(6, 5, map)).GrowingPlant = wheatSprout;
		}

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

		return result;
	}

    public void AgeMap(Map map, bool isInitialGeneration = false)
    {
        var originalGrid = map.CopyGrid();

        for (int x = 0; x < originalGrid.GetLength(0); x++)
        {
            for (int y = 0; y < originalGrid.GetLength(0); y++)
            {
                // Add surrounding pieces of soil to the context if they are available
                var contextSoils = GetContextSoils(x, y, originalGrid);

				var soil = map.Grid[x, y];
				if (soil.GrowingPlant != null)
					soil.GrowingPlant.IncreaseAge();

				if (isInitialGeneration)
					_soilFactory.SmoothSoil(soil, contextSoils, ExtremesWeight, 0);
				else
					_soilFactory.SmoothSoil(soil, contextSoils, ExtremesWeight, SoilOffsetPercentChance);
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
