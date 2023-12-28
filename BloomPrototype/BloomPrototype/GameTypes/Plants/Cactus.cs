using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Cactus : Plant
{
	public Cactus() { }

	List<Sticker>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Struggling;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

	public const int LifespanDays = 1 * 12 * 30;
}