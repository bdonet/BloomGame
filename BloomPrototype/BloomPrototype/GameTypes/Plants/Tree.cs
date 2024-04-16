using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes.Plants;

public class Tree : Plant
{
	List<Acorn>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Thriving;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Tight;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Moist;

	public const int LifespanDays = 100 * 4 * 30;
	public const int MaxDaysInEachMaturity = LifespanDays / 5;

	public Tree(Map map,
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