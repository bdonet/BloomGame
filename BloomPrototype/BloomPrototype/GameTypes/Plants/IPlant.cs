using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public interface IPlant
{
    public PlantMaturity Maturity { get; set; }

    public ISoil HostSoil { get; set; }

    public static ISoil SoilPreference;

    public static ISeed Fruit;

    public static int LifespanDays;

    public void GrowFruit();
}