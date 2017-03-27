using System.Linq;
using System.Collections.Generic;

namespace Bash
{
	public struct Position
	{
		public int X;
		public int Y;
	}

	public class Submap<T> where T : class
	{
		private class Row
		{
			private List<T> columns;

			public Row(int length, T defaultValue)
			{
				this.columns = Enumerable.Repeat(defaultValue, length).ToList();
			}

			public T GetAt(int column)
			{
				return this.columns[column];
			}
		}

		private List<Row> Rows;

		public T GetAt(Position position)
		{
			return this.Rows[position.Y].GetAt(position.X);
		}

		public Submap(int width, int height, T defaultValue = null)
		{
			this.Rows = Enumerable.Repeat(new Row(width, defaultValue), height).ToList();
		}
	}
	public class Map
	{
		Submap<Terrain> Terrain;
		Submap<Item> Items;
		Submap<Character> Characters;

		public Map(int width, int height)
		{
			this.Terrain = new Submap<Terrain>(width, height);
			this.Items = new Submap<Item>(width, height);
			this.Characters = new Submap<Character>(width, height);
		}
	}

	public class BashMapGenerator
	{
		public Map Generate()
		{
			return new Map(80, 25);
		}
	}
}
