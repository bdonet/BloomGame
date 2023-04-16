using Plant = BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Tomato : ISeed
{
    public ISoil? HostSoil { get; set; }
    public bool IsSprouting { get; set; }

    public static Type PlantType = typeof(Plant.Tomato);

    public void Sprout() => throw new NotImplementedException();
}