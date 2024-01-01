using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public class Tree : Plant
{
	List<Acorn>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Thriving;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Tight;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Moist;

	public const int LifespanDays = 60 * 12 * 30;

	public Tree(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health)
			: base(map, locationX, locationY, maturity, health) { }

	public override void IncreaseAge() { throw new NotImplementedException(); }
}