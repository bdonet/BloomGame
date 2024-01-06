using BloomPrototype.GameTypes.Soils;
using BloomPrototype.Services;

namespace BloomPrototype.GameTypes.Plants;

public class Tomato : Plant
{
	List<Seeds.Tomato>? Fruit;

	public const SoilFertility SoilFertilityPreference = SoilFertility.Alive;

	public const SoilRetention SoilRetentionPreference = SoilRetention.Loose;

	public const SoilWaterLevel SoilWaterLevelPreference = SoilWaterLevel.Wet;

	public const int LifespanDays = 2 * 12 * 30;

	readonly IRandomNumberGenerator random;

	public Tomato(Map map,
				int locationX,
				int locationY,
				PlantMaturity maturity,
				PlantHealth health,
				int daysInCurrentMaturity,
				IRandomNumberGenerator random)
			: base(map, locationX, locationY, maturity, health, daysInCurrentMaturity)
	{
		this.random = random;
	}

	public override void IncreaseAge()
	{
		throw new NotImplementedException();
	}
}