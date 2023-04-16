using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Seeds;

namespace BloomPrototype.GameTypes.Soils;

public class Sand : ISoil
{
    public SoilWaterLevel WaterLevel { get; set; }
    public SoilFertility Fertility { get; set; }
    public IPlant? GrowingPlant { get; set; }
    public ISeed? GrowingSeed { get; set; }

    public static SoilRetention Retention = SoilRetention.Loose;

    public void Fertilize(int levels) => throw new NotImplementedException();
    public void Water(int levels) => throw new NotImplementedException();
}