using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tree : Plant
{
    public Tree(Soil hostSoil)
    {
        HostSoil = hostSoil;
        Maturity = PlantMaturity.Seedling;
    }

    private List<Acorn>? Fruit;

    public const SoilFertility SoilFertilityPreference = SoilFertility.Thriving;

    public const SoilRetention SoilRetentionPreference = SoilRetention.Tight;

    public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Moist;

    public const int LifespanDays = 60 * 12 * 30;

    public override void GrowFruit() => throw new NotImplementedException();
    public override List<ISeed>? GetCurrentFruit() => throw new NotImplementedException();
    public override PlantMaturity GetMaturity() => throw new NotImplementedException();
    public override Soil GetHostSoil() => throw new NotImplementedException();
}