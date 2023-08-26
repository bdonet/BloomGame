namespace BloomPrototype.Services;

public interface ISoilFactory
{
	void SmoothSoil(GameTypes.Soils.Soil currentSoil, List<GameTypes.Soils.Soil>? contextSoils);
	GameTypes.Soils.Soil GenerateSoil();
}
