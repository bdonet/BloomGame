using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Wheat : IPlant
{
    public Wheat(ISoil hostSoil)
    {
        HostSoil = hostSoil;
    }

    public PlantMaturity Maturity { get; set; } = PlantMaturity.Seedling;
    public ISoil HostSoil { get; set; }

    public static ISoil SoilPreference;
    public static int LifespanDays = 2 * 12 * 30;
    public Grain? Fruit;

    public void GrowFruit() => throw new NotImplementedException();
}