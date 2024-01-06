using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;
using System;

namespace BloomPrototype.GameTypes.Plants;

public abstract class Plant : SurfaceObject
{
	private readonly IRandomNumberGenerator random;

	public Plant(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health, int daysInCurrentMaturity, IRandomNumberGenerator random)
			: base(map, locationX, locationY)
	{
		DaysInCurrentMaturity = daysInCurrentMaturity;
		this.random = random;
		Maturity = maturity;
		Health = health;
	}

	public PlantMaturity Maturity { get; protected set; }

	public PlantHealth Health { get; protected set; }

	public int DaysInCurrentMaturity { get; protected set; }

	public abstract void IncreaseAge();

	protected void IncreaseAge(int maxDaysInEachMaturity, SoilRetention retentionPreference, SoilWaterLevel waterLevelPreference, SoilFertility fertilityPreference)
	{
		if (Health == PlantHealth.Dead)
			// Plant is at the end of its lifespan. No need to change maturity or health.
			return;

		if (DaysInCurrentMaturity >= maxDaysInEachMaturity || CanIncreaseMaturity(maxDaysInEachMaturity))
		{
			// Increase plant maturity
			DaysInCurrentMaturity = 0;

			if (Maturity == PlantMaturity.Old)
			{
				// Plant is at the end of its lifespan. Kill it.
				Health = PlantHealth.Dead;
				return;
			}

			var maturityValue = (int)Maturity;
			Maturity = (PlantMaturity)(maturityValue + 1);
		}
		else
			// Not increasing maturity. Increment the number of days this plant has been in this maturity.
			DaysInCurrentMaturity++;

		// Increase plant health if possible
		var healthValue = (int)Health;
		var retentionDifference = (int)Location.Retention - retentionPreference;
		var waterLevelDifference = (int)Location.WaterLevel - waterLevelPreference;
		var fertilityDifference = (int)Location.Fertility - fertilityPreference;
		if (Math.Abs((decimal)retentionDifference)
			+ Math.Abs((decimal)waterLevelDifference)
			+ Math.Abs((decimal)fertilityDifference)
			> 3)
			Health = (PlantHealth)(healthValue - 1);
		else if (Health != PlantHealth.Thriving)
			// Health cannot improve if maxed out.
			Health = (PlantHealth)(healthValue + 1);
	}

	private bool CanIncreaseMaturity(int maxDaysInEachMaturity)
	{
		var roll = random.GenerateInt(1, maxDaysInEachMaturity - DaysInCurrentMaturity);
		return roll == 1;
	}
}