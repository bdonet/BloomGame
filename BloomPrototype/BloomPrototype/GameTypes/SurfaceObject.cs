﻿using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public abstract class SurfaceObject : ISurfaceObject
{
	public SurfaceObject(Map map, int locationX, int locationY)
	{
		Map = map;
		LocationX = locationX;
		LocationY = locationY;
	}

	protected readonly Map Map;
	protected int LocationX;
	protected int LocationY;

	public Soil Location => Map.GetSoil(new MapCoordinate(LocationX, LocationY, Map));
}
