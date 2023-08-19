using BloomPrototype.GameTypes.Seeds;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Plants;

public interface IPlant
{
    public PlantMaturity GetMaturity();

    public Soil GetHostSoil();

    public void GrowFruit();

    public List<ISeed>? GetCurrentFruit();
}