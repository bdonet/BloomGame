using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes.Plants;

public class Cactus : Plant
{
	List<Sticker>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Struggling;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

	public const int LifespanDays = 1 * 12 * 30;

	public const int MaxDaysInEachMaturity = LifespanDays / 5;

	readonly IRandomNumberGenerator random;

	public Cactus(Map map,
				int locationX,
				int locationY,
				PlantMaturity maturity,
				PlantHealth health,
				int daysInCurrentMaturity,
				IRandomNumberGenerator random)
			: base(map, locationX, locationY, maturity, health, daysInCurrentMaturity)
	{
		this.random = random;
	}

	public override void IncreaseAge()
	{
		if (Health == PlantHealth.Dead)
			// Plant is at the end of its lifespan. No need to change maturity or health.
			return;

		// Increase plant maturity
		if (DaysInCurrentMaturity >= MaxDaysInEachMaturity || CanIncreaseMaturity())
		{
			if (Maturity == PlantMaturity.Old)
			{
				// Plant is at the end of its lifespan. Kill it.
				Health = PlantHealth.Dead;
				return;
			}

			var maturityValue = (int)Maturity;
			Maturity = (PlantMaturity)(maturityValue + 1);
		}

		// Increase plant health if possible
		var healthValue = (int)Health;
		var retentionDifference = (int)Location.Retention - SoilRetentionPreference;
		var waterLevelDifference = (int)Location.WaterLevel - SoilWaterLevelPreference;
		var fertilityDifference = (int)Location.Fertility - SoilFertilityPreference;
		if (Math.Abs((decimal)retentionDifference)
			+ Math.Abs((decimal)waterLevelDifference)
			+ Math.Abs((decimal)fertilityDifference)
			> 3)
			Health = (PlantHealth)(healthValue - 1);
		else if (Health != PlantHealth.Thriving)
			// Health cannot improve if maxed out.
			Health = (PlantHealth)(healthValue + 1);
	}

	bool CanIncreaseMaturity()
	{
		var roll = random.GenerateInt(1, MaxDaysInEachMaturity - DaysInCurrentMaturity);
		return roll == 1;
	}
}