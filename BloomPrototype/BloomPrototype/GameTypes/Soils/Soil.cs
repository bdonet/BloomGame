using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Seeds;

namespace BloomPrototype.GameTypes.Soils;

public class Soil
{
    public SoilWaterLevel WaterLevel { get; set; }
    public SoilFertility Fertility { get; set; }
    public SoilRetention Retention { get; set; }
    public Plant? GrowingPlant { get; set; }
    public Seed? GrowingSeed { get; set; }

    public void Fertilize(int levels) => throw new NotImplementedException();
    public void Water(int levels) => throw new NotImplementedException();
}
