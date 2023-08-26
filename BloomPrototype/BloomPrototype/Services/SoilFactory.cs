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

	public void SmoothSoil(Soil currentSoil, List<Soil>? contextSoils)
	{
		if (contextSoils == null)
			return;

		double averageFertilityValue = (int)currentSoil.Fertility;
		double averageWaterLevelValue = (int)currentSoil.WaterLevel;
		double averageRetentionValue = (int)currentSoil.Retention;
		foreach (var context in contextSoils)
		{
			averageFertilityValue += (int)context.Fertility;
			averageWaterLevelValue += (int)context.WaterLevel;
			averageRetentionValue += (int)context.Retention;
		}

		averageFertilityValue /= contextSoils.Count + 1;
		averageWaterLevelValue /= contextSoils.Count + 1;
		averageRetentionValue /= contextSoils.Count + 1;

		averageFertilityValue = Math.Round(averageFertilityValue);
		averageWaterLevelValue = Math.Round(averageWaterLevelValue);
		averageRetentionValue = Math.Round(averageRetentionValue);

		currentSoil.Fertility = (SoilFertility)averageFertilityValue;
		currentSoil.WaterLevel = (SoilWaterLevel)averageWaterLevelValue;
		currentSoil.Retention = (SoilRetention)averageRetentionValue;
	}
}
