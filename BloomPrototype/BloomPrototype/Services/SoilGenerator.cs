using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.Services;

public class SoilGenerator : ISoilGenerator
{
	readonly IRandomNumberGenerator Random;

	public SoilGenerator(IRandomNumberGenerator random)
	{
		Random = random;
	}

	public Soil Generate()
	{
		var randomFertility = Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Overgrown);
		return new Soil
		{
			Fertility = (SoilFertility)randomFertility
		};
	}
}
