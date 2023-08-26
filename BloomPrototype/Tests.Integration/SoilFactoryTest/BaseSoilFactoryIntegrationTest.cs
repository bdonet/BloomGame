using BloomPrototype.Services;

public class BaseSoilFactoryIntegrationTest
{
	public BaseSoilFactoryIntegrationTest()
	{
		Generator = new RandomNumberGenerator();
	}

	internal IRandomNumberGenerator Generator;
}
