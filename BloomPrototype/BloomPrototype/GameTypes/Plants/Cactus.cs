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

	public Cactus(Map map,
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