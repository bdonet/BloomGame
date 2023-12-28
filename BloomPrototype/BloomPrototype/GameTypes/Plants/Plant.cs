namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : SurfaceObject
{
	public Plant(Map map, int locationX, int locationY, PlantMaturity maturity)
			: base(map, locationX, locationY)
	{ Maturity = maturity; }

	public PlantMaturity Maturity { get; protected set; }
}