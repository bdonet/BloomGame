using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Seeds;

namespace BloomPrototype.GameTypes.Soils;

public interface ISoil
{
    public SoilWaterLevel WaterLevel { get; set; }

    public SoilFertility Fertility { get; set; }

    public IPlant? GrowingPlant { get; set; }

    public ISeed? GrowingSeed { get; set; }

    public static SoilRetention WaterRetention;

    public void Water(int levels);

    public void Fertilize(int levels);
}