using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Acorn : ISeed
{
    public ISoil? HostSoil { get; set; }
    public bool IsSprouting { get; set; }

    public static Type PlantType => typeof(Tree);

    public void Sprout() => throw new NotImplementedException();
}
