using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tomato : Plant
{
	List<Seeds.Tomato>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Wet;

	public const int LifespanDays = 2 * 12 * 30;

	public Tomato(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health)
			: base(map, locationX, locationY, maturity, health) { }

	public override void IncreaseAge() { throw new NotImplementedException(); }
}