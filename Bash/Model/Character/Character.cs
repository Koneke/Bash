namespace Bash
{
	public class Character
	{
		public Map Map;
		public Position Position => this.Map.Characters.PositionOf(this);

		public Character(Map map)
		{
			this.Map = map;
		}
	}
}
