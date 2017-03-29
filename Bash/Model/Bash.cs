using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bash
{
	public class Vec2i
	{
		public int X;
		public int Y;

		public Vec2i(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public static Vec2i operator +(Vec2i a, Vec2i b)
		{
			return new Vec2i(a.X + b.X, a.Y + b.Y);
		}
	}

	public class Position
	{
		public Vec2i V;
		public int X => this.V.X;
		public int Y => this.V.Y;

		public Position(int x, int y)
			: this(new Vec2i(x, y))
		{
		}

		public Position(Vec2i position)
		{
			this.V = position;
		}

		public static Position operator +(Position p, Position q)
		{
			return new Position(p.V + q.V);
		}

		public static Position operator +(Position p, Direction q)
		{
			return new Position(p.V + q.V);
		}
	}

	public class Direction
	{
		public Vec2i V;
		public int Dx => this.V.X;
		public int Dy => this.V.Y;

		public Direction(int x, int y)
			: this(new Vec2i(x, y))
		{
		}

		public Direction(Vec2i position)
		{
			this.V = position;
		}

		public static Direction Normalise(Direction d)
		{
			throw new System.NotImplementedException();
		}
	}

	// Commands are created by the Controller (Brain for AI),
	// and then *applied* to characters.

	public class MovementCommand : Command
	{
		private Direction direction;

		public MovementCommand(Direction direction)
		{
			this.direction = direction;
		}

		public override void ApplyTo(Character character)
		{
			var fromTerrain = character.Map.Terrain.GetAt(character.Position);
			var toTerrain = character.Map.Terrain.GetAt(character.Position + direction);
		}
	}

	public abstract class Command
	{
		public abstract void ApplyTo(Character character);
	}

	public class Terrain
	{
		public int Elevation; // 0 = sea level
	}

	// This is our model class,
	// it should *primarily consist of public fields*,
	// and *public methods manipulating those fields*,
	// and of course,
	// the *private fields/methods providing the above*.
	public class Bash
	{
		public Map Map;

		private BashMapGenerator mapGenerator;

		public Bash()
		{
			this.mapGenerator = new BashMapGenerator();
			this.Map = mapGenerator.Generate();
		}
	}
}
