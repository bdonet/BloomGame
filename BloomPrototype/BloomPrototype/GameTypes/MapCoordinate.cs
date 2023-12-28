namespace BloomPrototype.GameTypes;

public class MapCoordinate
{
	public MapCoordinate(int x, int y)
	{
		if (x < 0 || y < 0)
			throw new ArgumentException($"Cannot create {nameof(MapCoordinate)} with negative coordinates");

		X = x;
		Y = y;
	}

	public int X;
	public int Y;
}
