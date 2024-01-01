using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Wheat : Plant
{
	public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Holding;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Dry;

	public const int LifespanDays = 2 * 12 * 30;

	List<Grain>? Fruit;

	public Wheat(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health)
			: base(map, locationX, locationY, maturity, health) { }

	public override void IncreaseAge() { throw new NotImplementedException(); }
}