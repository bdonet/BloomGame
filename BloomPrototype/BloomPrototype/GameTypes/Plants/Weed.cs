using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Weed : IPlant
{
    public Weed(ISoil hostSoil)
    {
        HostSoil = hostSoil;
    }

    public PlantMaturity Maturity { get; set; } = PlantMaturity.Seedling;
    public ISoil HostSoil { get; set; }

    private List<Sticker>? Fruit { get; set; }

    public static ISoil SoilPreference;
    public static int LifespanDays;

    public void GrowFruit() => throw new NotImplementedException();

    public List<ISeed> GetCurrentFruit() => throw new NotImplementedException();
}