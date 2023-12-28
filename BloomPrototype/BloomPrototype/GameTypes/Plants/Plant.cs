namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : SurfaceObject
{
	public Plant(Map map, int locationX, int locationY) : base(map, locationX, locationY) { }

	protected PlantMaturity Maturity { get; set; }
}