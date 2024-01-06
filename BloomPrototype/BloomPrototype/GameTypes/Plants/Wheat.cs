using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes.Plants;

public class Wheat : Plant
{
	public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Holding;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

	public const int LifespanDays = 2 * 12 * 30;
	public const int MaxDaysInEachMaturity = LifespanDays / 5;

	List<Grain>? Fruit;

	public Wheat(Map map,
				int locationX,
				int locationY,
				PlantMaturity maturity,
				PlantHealth health,
				int daysInCurrentMaturity,
				IRandomNumberGenerator random)
			: base(map, locationX, locationY, maturity, health, daysInCurrentMaturity, random) { }

	public override void IncreaseAge()
	{
		IncreaseAge(MaxDaysInEachMaturity, SoilRetentionPreference, SoilWaterLevelPreference, SoilFertilityPreference);
	}
}