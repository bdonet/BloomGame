namespace BloomPrototype.GameTypes;

public class Player : SurfaceObject
{
	readonly int _actionsPerDay;

	public Player(Map map, int locationX, int locationY, int actionsPerDay)
			: base(map, locationX, locationY)
	{
		Actions = actionsPerDay;
		_actionsPerDay = actionsPerDay;
	}

	public void FertilizeSoil(int levels)
	{
		if (Actions < 1)
			return;

		Location.Fertilize(levels);
		Actions--;
	}

	public void MoveDown()
	{
		if (LocationY != Map.Grid.GetUpperBound(1))
			LocationY++;
	}

	public void MoveLeft()
	{
		if (LocationX != 0)
			LocationX--;
	}

	public void MoveRight()
	{
		if (LocationX != Map.Grid.GetUpperBound(0))
			LocationX++;
	}

	public void MoveUp()
	{
		if (LocationY != 0)
			LocationY--;
	}

	public void Sleep() { Actions = _actionsPerDay; }

	public void TightenSoil(int levels)
	{
		if (Actions < 1)
			return;

		Location.Tighten(levels);
		Actions--;
	}

	public void WaterSoil(int levels)
	{
		if (Actions < 1)
			return;

		Location.Water(levels);
		Actions--;
	}

	public int Actions { get; private set; }
}
