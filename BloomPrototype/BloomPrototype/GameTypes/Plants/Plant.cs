namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : SurfaceObject
{
	public Plant(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health)
			: base(map, locationX, locationY)
	{
		Maturity = maturity;
		Health = health;
	}

	public PlantMaturity Maturity { get; protected set; }

	public PlantHealth Health { get; protected set; }

	public abstract void IncreaseAge();
}