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
		var randomFertility = Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);
		return new Soil
		{
			Fertility = (SoilFertility)randomFertility,
			WaterLevel = (SoilWaterLevel)randomFertility,
			Retention = (SoilRetention)randomFertility
		};
	}
}
