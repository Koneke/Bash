using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bash
{
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
