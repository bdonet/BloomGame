using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Grain : ISeed
{
    public ISoil? HostSoil { get; set; }
    public bool IsSprouting { get; set; }

    public static Type PlantType = typeof(Plants.Wheat);

    public void Sprout() => throw new NotImplementedException();
}