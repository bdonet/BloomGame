using BloomPrototype.GameTypes.Soils;

namespace BloomPrototype.GameTypes;

public class Player
{
	int _locationX;

	int _locationY;

	readonly Map _map;

	public int Actions { get; private set; }

	public Player(Map map)
	{
		_map = map;
		_locationX = 1;
		_locationY = 1;
	}

	public void FertilizeSoil(int levels) { Location.Fertilize(levels); }

	public void MoveDown()
	{
		if (_locationY != _map.Grid.GetUpperBound(1))
			_locationY++;
	}

	public void MoveLeft()
	{
		if (_locationX != 0)
			_locationX--;
	}

	public void MoveRight()
	{
		if (_locationX != _map.Grid.GetUpperBound(0))
			_locationX++;
	}

	public void MoveUp()
	{
		if (_locationY != 0)
			_locationY--;
	}

	public void Sleep() { Actions = 10; }

	public void TightenSoil(int levels) { Location.Tighten(levels); }

	public void WaterSoil(int levels) { Location.Water(levels); }

	public Soil Location => _map.GetSoil(_locationX, _locationY);
}
