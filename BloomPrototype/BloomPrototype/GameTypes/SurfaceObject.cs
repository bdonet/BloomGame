namespace BloomPrototype.GameTypes;

public abstract class SurfaceObject
{
	public SurfaceObject(Map map, uint locationX, uint locationY) { }

	protected readonly Map Map;
	protected uint LocationX;
	protected uint LocationY;
}
