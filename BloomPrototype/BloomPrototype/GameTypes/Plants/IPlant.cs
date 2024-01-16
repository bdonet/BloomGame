namespace BloomPrototype.GameTypes.Plants;

public interface IPlant : ISurfaceObject
{
	PlantMaturity Maturity { get; }

	PlantHealth Health { get; }

	int DaysInCurrentMaturity { get; }

	void IncreaseAge();
}
