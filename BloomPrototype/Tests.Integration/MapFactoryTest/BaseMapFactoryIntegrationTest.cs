using BloomPrototype.Services;

namespace Tests.Integration.MapFactoryTest;

public class BaseMapFactoryIntegrationTest
{
	public BaseMapFactoryIntegrationTest()
	{
		SoilFactory = new SoilFactory(new RandomNumberGenerator());
	}

	internal ISoilFactory SoilFactory;
}
