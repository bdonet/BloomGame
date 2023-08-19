using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Weed : Plant
{
    public Weed(Soil hostSoil)
    {
        HostSoil = hostSoil;
        Maturity = PlantMaturity.Seedling;
    }

    private List<Sticker>? Fruit;

    public const SoilFertility SoilFertilityPreference = SoilFertility.Struggling;

    public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

    public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

    public const int LifespanDays = 1 * 12 * 60;

    public override void GrowFruit() => throw new NotImplementedException();
    public override List<Seed> GetCurrentFruit() => throw new NotImplementedException();
    public override PlantMaturity GetMaturity() => throw new NotImplementedException();
    public override Soil GetHostSoil() => throw new NotImplementedException();
}