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

	public void SmoothSoil(Soil currentSoil, List<Soil>? contextSoils, double extremesWeight)
	{
		if (contextSoils == null)
			return;

		currentSoil.Fertility = CalculateSmoothedSoilFertility(currentSoil, contextSoils, extremesWeight);
		currentSoil.WaterLevel = CalculateSmoothedSoilWaterLevel(currentSoil, contextSoils, extremesWeight);
		currentSoil.Retention = CalculateSmoothedSoilRetention(currentSoil, contextSoils, extremesWeight);
	}

	private SoilFertility CalculateSmoothedSoilFertility(Soil currentSoil, List<Soil> contextSoils, double extremesWeight)
	{
		var averageFertilityValue = (double)currentSoil.Fertility;

		// Weight the extremes so the smoothing tends toward the extremes when they are present
		if (currentSoil.Fertility == SoilFertility.Dead)
			averageFertilityValue -= extremesWeight;
		if (currentSoil.Fertility == SoilFertility.Overgrown)
			averageFertilityValue += extremesWeight / 2;

		foreach (var context in contextSoils)
		{
			averageFertilityValue += (double)context.Fertility;

			if (context.Fertility == SoilFertility.Dead)
				averageFertilityValue -= extremesWeight;
			if (context.Fertility == SoilFertility.Overgrown)
				averageFertilityValue += extremesWeight / 2;
		}

		averageFertilityValue /= contextSoils.Count + 1;

		var resultFertilityValue = Math.Round(averageFertilityValue);

		if (resultFertilityValue < (int)SoilFertility.Dead)
			return SoilFertility.Dead;

		if (resultFertilityValue > (int)SoilFertility.Overgrown)
			return SoilFertility.Overgrown;

		return (SoilFertility)resultFertilityValue;
	}

	private SoilWaterLevel CalculateSmoothedSoilWaterLevel(Soil currentSoil, List<Soil> contextSoils, double extremesWeight)
	{
		var averageWaterLevelValue = (double)currentSoil.WaterLevel;

		// Weight the extremes so the smoothing tends toward the extremes when they are present
		if (currentSoil.WaterLevel == SoilWaterLevel.Parched)
			averageWaterLevelValue -= extremesWeight;
		if (currentSoil.WaterLevel == SoilWaterLevel.Flooded)
			averageWaterLevelValue += extremesWeight / 2;

		foreach (var context in contextSoils)
		{
			averageWaterLevelValue += (double)context.WaterLevel;

			if (context.WaterLevel == SoilWaterLevel.Parched)
				averageWaterLevelValue -= extremesWeight;
			if (context.WaterLevel == SoilWaterLevel.Flooded)
				averageWaterLevelValue += extremesWeight / 2;
		}

		averageWaterLevelValue /= contextSoils.Count + 1;

		var resultWaterLevelValue = Math.Round(averageWaterLevelValue);

		if (resultWaterLevelValue < (int)SoilWaterLevel.Parched)
			return SoilWaterLevel.Parched;

		if (resultWaterLevelValue > (int)SoilWaterLevel.Flooded)
			return SoilWaterLevel.Flooded;

		return (SoilWaterLevel)resultWaterLevelValue;
	}

	private SoilRetention CalculateSmoothedSoilRetention(Soil currentSoil, List<Soil> contextSoils, double extremesWeight)
	{
		var averageRetentionValue = (double)currentSoil.Retention;

		// Weight the extremes so the smoothing tends toward the extremes when they are present
		if (currentSoil.Retention == SoilRetention.Dust)
			averageRetentionValue -= extremesWeight;
		if (currentSoil.Retention == SoilRetention.Packed)
			averageRetentionValue += extremesWeight / 2;

		foreach (var context in contextSoils)
		{
			averageRetentionValue += (double)context.Retention;

			if (context.Retention == SoilRetention.Dust)
				averageRetentionValue -= extremesWeight;
			if (context.Retention == SoilRetention.Packed)
				averageRetentionValue += extremesWeight / 2;
		}

		averageRetentionValue /= contextSoils.Count + 1;

		var resultRetentionValue = Math.Round(averageRetentionValue);

		if (resultRetentionValue < (int)SoilRetention.Dust)
			return SoilRetention.Dust;

		if (resultRetentionValue > (int)SoilRetention.Packed)
			return SoilRetention.Packed;

		return (SoilRetention)resultRetentionValue;
	}
}
