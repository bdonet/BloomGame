using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Wheat : Plant
{
    public Wheat(ISoil hostSoil)
    {
        HostSoil = hostSoil;
        Maturity = PlantMaturity.Seedling;
    }

    public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

    public const SoilRetention SoilRetentionPreference = SoilRetention.Holding;

    public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

    public const int LifespanDays = 2 * 12 * 30;

    private List<Grain>? Fruit;

    public override void GrowFruit() => throw new NotImplementedException();
    public override List<ISeed>? GetCurrentFruit() => throw new NotImplementedException();
    public override PlantMaturity GetMaturity() => throw new NotImplementedException();
    public override ISoil GetHostSoil() => throw new NotImplementedException();
}