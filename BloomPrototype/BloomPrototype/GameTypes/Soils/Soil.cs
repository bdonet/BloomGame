using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Seeds;

namespace BloomPrototype.GameTypes.Soils;

public class Soil
{
	public void Fertilize(int levels)
	{
		var integerValue = (int)Fertility + levels;

		if (integerValue > (int)SoilFertility.Overgrown)
			Fertility = SoilFertility.Overgrown;
		else if (integerValue < (int)SoilFertility.Dead)
			Fertility = SoilFertility.Dead;
		else
			Fertility = (SoilFertility)integerValue;
	}

	public void Tighten(int levels)
	{
		var integerValue = (int)Retention + levels;

		if (integerValue > (int)SoilRetention.Packed)
			Retention = SoilRetention.Packed;
		else if (integerValue < (int)SoilRetention.Dust)
			Retention = SoilRetention.Dust;
		else
			Retention = (SoilRetention)integerValue;
	}

	public void Water(int levels)
	{
		var integerValue = (int)WaterLevel + levels;

		if (integerValue > (int)SoilWaterLevel.Flooded)
			WaterLevel = SoilWaterLevel.Flooded;
		else if (integerValue < (int)SoilWaterLevel.Parched)
			WaterLevel = SoilWaterLevel.Parched;
		else
			WaterLevel = (SoilWaterLevel)integerValue;
	}

	public SoilFertility Fertility { get; set; }

	public IPlant? GrowingPlant { get; set; }

	public Seed? GrowingSeed { get; set; }

	public SoilRetention Retention { get; set; }

	public SoilWaterLevel WaterLevel { get; set; }
}
