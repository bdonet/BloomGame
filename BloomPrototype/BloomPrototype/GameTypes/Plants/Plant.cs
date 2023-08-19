using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : IPlant
{
    protected PlantMaturity Maturity { get; set; }

    protected ISoil HostSoil { get; set; }

    public abstract PlantMaturity GetMaturity();
    public abstract ISoil GetHostSoil();
    public abstract void GrowFruit();
    public abstract List<ISeed>? GetCurrentFruit();
}