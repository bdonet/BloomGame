namespace BloomPrototype.Services;

public interface ISoilFactory
{
	void SmoothSoil(GameTypes.Soils.Soil currentSoil, List<GameTypes.Soils.Soil>? contextSoils, double extremesWeight, int soilOffsetPercentChance);
	GameTypes.Soils.Soil GenerateSoil();
}
