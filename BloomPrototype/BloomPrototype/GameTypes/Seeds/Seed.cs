using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public abstract class Seed : ISeed
{
    protected abstract ISoil? HostSoil { get; set; }

    public abstract void Sprout();
}