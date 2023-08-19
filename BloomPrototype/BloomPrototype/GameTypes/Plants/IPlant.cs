using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public interface IPlant
{
    public PlantMaturity Maturity { get; set; }

    public ISoil HostSoil { get; set; }

    public static SoilFertility SoilFertilityPreference;

    public static SoilRetention SoilRetentionPreference;

    public static SoilWaterLevel SoilWaterLevelPreference;

    public static int LifespanDays;

    public void GrowFruit();

    public List<ISeed>? GetCurrentFruit();
}