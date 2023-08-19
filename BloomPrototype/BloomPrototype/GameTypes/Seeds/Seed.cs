using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public abstract class Seed : ISeed
{
    protected abstract Soil? HostSoil { get; set; }

    public abstract void Sprout();
}