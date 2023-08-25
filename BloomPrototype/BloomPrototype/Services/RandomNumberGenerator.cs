namespace BloomPrototype.Services;

public class RandomNumberGenerator : IRandomNumberGenerator
{
	Random Random => new Random();

	public int GenerateInt(int lowInclusive, int highInclusive)
	{
		return Random.Next(lowInclusive, highInclusive + 1);
	}
}
