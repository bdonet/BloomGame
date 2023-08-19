using BloomPrototype.GameTypes.Plants;
using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes.Seeds;

public interface ISeed
{
    public static Type PlantType;

    public void Sprout();
}