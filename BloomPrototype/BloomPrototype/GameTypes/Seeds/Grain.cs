using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public class Grain : Seed
{
    protected override Soil? HostSoil { get; set; }

    public static Type PlantType = typeof(Wheat);

    public override void Sprout() => throw new NotImplementedException();
}