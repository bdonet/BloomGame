using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant
{
	protected Soil HostSoil { get; set; }

	protected PlantMaturity Maturity { get; set; }
}