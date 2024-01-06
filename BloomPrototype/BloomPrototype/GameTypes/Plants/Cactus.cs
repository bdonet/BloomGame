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

	public Cactus(Map map, int locationX, int locationY, PlantMaturity maturity, PlantHealth health)
			: base(map, locationX, locationY, maturity, health) { }

	public override void IncreaseAge()
	{
		// Increase plant maturity
		if (Maturity == PlantMaturity.Old)
		{
			// Plant is at the end of its lifespan
			Health = PlantHealth.Dead;
			return;
		}

		var maturityValue = (int)Maturity;
		Maturity = (PlantMaturity)(maturityValue + 1);

		// Increase plant health if possible
		if (Health == PlantHealth.Dead)
			// Health cannot change if already dead.
			return;

		var healthValue = (int)Health;
		if (Location.Retention == SoilRetentionPreference && Location.WaterLevel == SoilWaterLevelPreference && Location.Fertility == SoilFertilityPreference)
		{
			if (Health != PlantHealth.Thriving)
				// Health cannot improve if maxed out.
				Health = (PlantHealth)(healthValue + 1);
		}
		else
			Health = (PlantHealth)(healthValue - 1);
	}
}