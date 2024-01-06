namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : SurfaceObject
{
	public Plant(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health, int daysInCurrentMaturity)
			: base(map, locationX, locationY)
	{
		DaysInCurrentMaturity = daysInCurrentMaturity;
		Maturity = maturity;
		Health = health;
	}

	public PlantMaturity Maturity { get; protected set; }

	public PlantHealth Health { get; protected set; }

	public int DaysInCurrentMaturity { get; protected set; }

	public abstract void IncreaseAge();
}