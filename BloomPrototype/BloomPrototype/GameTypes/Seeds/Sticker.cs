using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Sticker : Seed
{
    protected override ISoil? HostSoil { get; set; }

    public static Type PlantType = typeof(Weed);

    public override void Sprout() => throw new NotImplementedException();
}