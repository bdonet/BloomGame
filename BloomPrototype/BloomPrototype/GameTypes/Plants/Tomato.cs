using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tomato : Plant
{
    public Tomato(Soil hostSoil)
    {
        HostSoil = hostSoil;
        Maturity = PlantMaturity.Seedling;
    }

    private List<Seeds.Tomato>? Fruit;

    public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

    public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

    public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Wet;

    public const int LifespanDays = 2 * 12 * 30;

    public override void GrowFruit() => throw new NotImplementedException();
    public override List<Seed>? GetCurrentFruit() => throw new NotImplementedException();
    public override PlantMaturity GetMaturity() => throw new NotImplementedException();
    public override Soil GetHostSoil() => throw new NotImplementedException();
}