using BloomPrototype.GameTypes.Soils;
using System.Diagnostics;

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
		var randomFertility = (SoilFertility)Random.GenerateInt((int)SoilFertility.Dead, (int)SoilFertility.Struggling);
		var randomWaterLevel = (SoilWaterLevel)Random.GenerateInt((int)SoilWaterLevel.Parched, (int)SoilWaterLevel.Dry);
		var randomRetention = (SoilRetention)Random.GenerateInt((int)SoilRetention.Dust, (int)SoilRetention.Packed);

		return new Soil
		{
			Fertility = randomFertility,
			WaterLevel = randomWaterLevel,
			Retention = randomRetention
		};
	}

	public void SmoothSoil(Soil currentSoil, List<Soil>? contextSoils, double extremesWeight, int soilOffsetPercentChange)
	{
		if (contextSoils == null)
			return;

		currentSoil.Fertility = CalculateSmoothedSoilFertility(currentSoil, contextSoils, extremesWeight, soilOffsetPercentChange);
		currentSoil.WaterLevel = CalculateSmoothedSoilWaterLevel(currentSoil, contextSoils, extremesWeight, soilOffsetPercentChange);
		currentSoil.Retention = CalculateSmoothedSoilRetention(currentSoil, contextSoils, extremesWeight, soilOffsetPercentChange);
	}

	private SoilFertility CalculateSmoothedSoilFertility(Soil currentSoil, List<Soil> contextSoils, double extremesWeight, int soilOffsetPercentChance)
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

		var resultFertilityValue = (int)Math.Round(averageFertilityValue);
		resultFertilityValue = RandomSoilOffset(resultFertilityValue, soilOffsetPercentChance);

		if (resultFertilityValue < (int)SoilFertility.Dead)
			return SoilFertility.Dead;

		if (resultFertilityValue > (int)SoilFertility.Overgrown)
			return SoilFertility.Overgrown;

		return (SoilFertility)resultFertilityValue;
	}

	private SoilWaterLevel CalculateSmoothedSoilWaterLevel(Soil currentSoil, List<Soil> contextSoils, double extremesWeight, int soilOffsetPercentChance)
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

		var resultWaterLevelValue = (int)Math.Round(averageWaterLevelValue);
		resultWaterLevelValue = RandomSoilOffset(resultWaterLevelValue, soilOffsetPercentChance);

		if (resultWaterLevelValue < (int)SoilWaterLevel.Parched)
			return SoilWaterLevel.Parched;

		if (resultWaterLevelValue > (int)SoilWaterLevel.Flooded)
			return SoilWaterLevel.Flooded;

		return (SoilWaterLevel)resultWaterLevelValue;
	}

	private SoilRetention CalculateSmoothedSoilRetention(Soil currentSoil, List<Soil> contextSoils, double extremesWeight, int soilOffsetPercentChance)
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

		var resultRetentionValue = (int)Math.Round(averageRetentionValue);
		resultRetentionValue = RandomSoilOffset(resultRetentionValue, soilOffsetPercentChance);

		if (resultRetentionValue < (int)SoilRetention.Dust)
			return SoilRetention.Dust;

		if (resultRetentionValue > (int)SoilRetention.Packed)
			return SoilRetention.Packed;

		return (SoilRetention)resultRetentionValue;
	}

	private int RandomSoilOffset(int soilValue, int soilOffsetPercentChance)
	{
		if (soilOffsetPercentChance > 50)
			throw new ArgumentException("Soil offset chance cannot be over 50");

		var maxValue = 100 / soilOffsetPercentChance;

		var randomNumber = Random.GenerateInt(1, maxValue);

		if (randomNumber == 1)
			return soilValue - 1;
		if (randomNumber == maxValue)
			return soilValue + 1;
		if (randomNumber > 1 && randomNumber < maxValue)
			return soilValue;

		throw new ArgumentException("Unsupported random soil offset");
	}
}
