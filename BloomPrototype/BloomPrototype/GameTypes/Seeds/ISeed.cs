using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public interface ISeed
{
    public static Type PlantType;

    public ISoil? HostSoil { get; set; }

    public bool IsSprouting { get; set; }

    public void Sprout();
}