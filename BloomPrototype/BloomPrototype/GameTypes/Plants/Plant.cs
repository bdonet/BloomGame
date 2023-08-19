using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : IPlant
{
    protected PlantMaturity Maturity { get; set; }

    protected Soil HostSoil { get; set; }

    public abstract PlantMaturity GetMaturity();
    public abstract Soil GetHostSoil();
    public abstract void GrowFruit();
    public abstract List<ISeed>? GetCurrentFruit();
}