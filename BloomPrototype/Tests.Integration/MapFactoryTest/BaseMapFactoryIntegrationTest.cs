using BloomPrototype.Services;
using Microsoft.Extensions.Configuration;
using Telerik.JustMock;

namespace Tests.Integration.MapFactoryTest;

public class BaseMapFactoryIntegrationTest
{
	public BaseMapFactoryIntegrationTest()
	{
		SoilFactory = new SoilFactory(new RandomNumberGenerator());

		Configuration = Mock.Create<IConfiguration>();
		Mock.Arrange(() => Configuration["LowerGridSizeBound"]).Returns(1.ToString());
		Mock.Arrange(() => Configuration["UpperGridSizeBound"]).Returns(1000.ToString());
		Mock.Arrange(() => Configuration["ContextRadius"]).Returns(2.ToString());
		Mock.Arrange(() => Configuration["ExtremesWeight"]).Returns(2.ToString());
		Mock.Arrange(() => Configuration["WorldSize"]).Returns(100.ToString());
		Mock.Arrange(() => Configuration["SoilOffsetPercentChance"]).Returns(20.ToString());
	}

	internal ISoilFactory SoilFactory;

	internal IConfiguration Configuration;
}
