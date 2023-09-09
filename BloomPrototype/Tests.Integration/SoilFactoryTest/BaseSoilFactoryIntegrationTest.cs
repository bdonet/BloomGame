using BloomPrototype.Services;

namespace Tests.Integration.SoilFactoryTest;

public class BaseSoilFactoryIntegrationTest
{
	public BaseSoilFactoryIntegrationTest()
	{
		Generator = new RandomNumberGenerator();
	}

	internal IRandomNumberGenerator Generator;
}
