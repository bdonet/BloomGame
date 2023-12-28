using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Cactus : Plant
{
	List<Sticker>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Struggling;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

	public const int LifespanDays = 1 * 12 * 30;

	public Cactus(Map map, int locationX, int locationY, PlantMaturity maturity)
			: base(map, locationX, locationY, maturity) { }
}