using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Acorn : Seed
{
    protected override ISoil? HostSoil { get; set; }

    public static Type PlantType => typeof(Tree);

    public override void Sprout() => throw new NotImplementedException();
}
