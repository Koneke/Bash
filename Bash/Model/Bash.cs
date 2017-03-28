using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bash
{
	public class Position
	{
		public int X;
		public int Y;

		public Position(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}

	public class Direction
	{
		public Direction(int dx, int dy)
		{
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
			throw new NotImplementedException();

			var terrain = character.Map.Terrain.GetAt(character.Position);
		}
	}

	public abstract class Command
	{
		public abstract void ApplyTo(Character character);
	}

	public class Terrain
	{
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
