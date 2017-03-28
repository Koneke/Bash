using System.Linq;
using System.Collections.Generic;

namespace Bash
{
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

			public int? ColumnOf(T item)
			{
				return this.columns.Contains(item)
					? (int?)columns.IndexOf(item)
					: null;
			}

			public bool Contains(T item)
			{
				return this.columns.Contains(item);
			}
		}

		private List<Row> Rows;

		public T GetAt(Position position)
		{
			return this.Rows[position.Y].GetAt(position.X);
		}

		public Position PositionOf(T item)
		{
			var r = this.Rows.FirstOrDefault(x => x.Contains(item));

			if (r == default(T))
			{
				return null;
			}

			var row = this.Rows.IndexOf(r);

			// We already know it's contained, so .Value
			var col = r.ColumnOf(item).Value;

			return new Position(col, row);
		}

		public Submap(int width, int height, T defaultValue = null)
		{
			this.Rows = Enumerable.Repeat(new Row(width, defaultValue), height).ToList();
		}
	}

	public class Map
	{
		public Submap<Terrain> Terrain;
		public Submap<Item> Items;
		public Submap<Character> Characters;

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
