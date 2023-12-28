namespace BloomPrototype.GameTypes;

public class MapCoordinate
{
	public MapCoordinate(int x, int y, Map map)
	{
		if ((x < 0) || (y < 0))
			throw new ArgumentException($"Cannot create {nameof(MapCoordinate)} with negative coordinates");

		var mapSize = map.Size;
		if ((x >= mapSize) || (y >= mapSize))
			throw new ArgumentException($"Cannot create {nameof(MapCoordinate)} beyond the edge of the map");

		X = x;
		Y = y;
		_Map = map;
	}

	public readonly int X;
	public readonly int Y;
	readonly Map _Map;

	public bool MatchesMap(Map map)
	{
		return map == _Map;
	}
}
