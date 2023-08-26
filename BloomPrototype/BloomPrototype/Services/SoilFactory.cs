using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.Services;

public class SoilFactory : ISoilFactory
{
	readonly IRandomNumberGenerator Random;

	public SoilFactory(IRandomNumberGenerator random)
	{
		Random = random;
	}

	public Soil GenerateSoil()
	{
		var randomFertility = (SoilFertility)Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);
		var randomWaterLevel = (SoilWaterLevel)Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);
		var randomRetention = (SoilRetention)Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);

		return new Soil
		{
			Fertility = randomFertility,
			WaterLevel = randomWaterLevel,
			Retention = randomRetention
		};
	}
}
