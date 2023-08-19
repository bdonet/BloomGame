using Plant = BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Tomato : Seed
{
    protected override ISoil? HostSoil { get; set; }

    public static Type PlantType = typeof(Plant.Tomato);

    public override void Sprout() => throw new NotImplementedException();
}