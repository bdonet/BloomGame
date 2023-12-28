using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tree : Plant
{
	public Tree() { }

	List<Acorn>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Thriving;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Tight;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Moist;

	public const int LifespanDays = 60 * 12 * 30;
}