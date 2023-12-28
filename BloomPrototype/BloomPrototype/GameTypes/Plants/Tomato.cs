using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tomato : Plant
{
	public Tomato() { }

	List<Seeds.Tomato>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Wet;

	public const int LifespanDays = 2 * 12 * 30;
}