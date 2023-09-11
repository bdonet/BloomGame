using BloomPrototype.Services;
using Microsoft.Extensions.Configuration;

namespace Tests.Integration.MapFactoryTest;

public class BaseMapFactoryIntegrationTest
{
	public BaseMapFactoryIntegrationTest()
	{
		SoilFactory = new SoilFactory(new RandomNumberGenerator());

		var builder = new ConfigurationBuilder();
		builder.Properties.Add("LowerGridSizeBound", 1.ToString());
		builder.Properties.Add("UpperGridSizeBound", 1000.ToString());
		builder.Properties.Add("ContextRadius", 2.ToString());
		builder.Properties.Add("ExtremesWeight", 2.ToString());
		builder.Properties.Add("WorldSize", 100.ToString());
		Configuration = builder.Build();
	}

	internal ISoilFactory SoilFactory;

	internal IConfiguration Configuration;
}
