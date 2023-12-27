using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Seeds;

namespace BloomPrototype.GameTypes.Soils;

public class Soil
{
	public void Fertilize(int levels)
	{
		var integerValue = ((int)Fertility) + levels;

		if (integerValue > (int)SoilFertility.Overgrown)
			Fertility = SoilFertility.Overgrown;
		else
			Fertility = (SoilFertility)integerValue;
	}

	public void Water(int levels) { throw new NotImplementedException(); }

	public SoilFertility Fertility { get; set; }

	public Plant? GrowingPlant { get; set; }

	public Seed? GrowingSeed { get; set; }

	public SoilRetention Retention { get; set; }

	public SoilWaterLevel WaterLevel { get; set; }
}
