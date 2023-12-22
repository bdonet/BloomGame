using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Player
{
	public Player(Map map)
	{
		Location = map.GetSoil(1, 1);
	}

	public Soil Location { get; private set; }

	public void MoveUp()
	{

	}

	public void MoveDown()
	{

	}

	public void MoveLeft()
	{

	}

	public void MoveRight()
	{

	}
}
