using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Player
{
	int _locationX;

	int _locationY;

	readonly Map _map;

	public Player(Map map)
	{
		_map = map;
		_locationX = 1;
		_locationY = 1;
	}

	public void MoveDown() { }

	public void MoveLeft() { }

	public void MoveRight() { }

	public void MoveUp() { }

	public Soil Location => _map.GetSoil(_locationX, _locationY);
}
