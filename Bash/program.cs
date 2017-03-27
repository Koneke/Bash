using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyProject
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

	public class Terrain
	{
	}

	public class Character
	{
	}

	public class Item
	{
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

	public class KeyEvent
	{
		public Keys Key;
		public bool Down;

		public KeyEvent(Keys key, bool down)
		{
			this.Key = key;
			this.Down = down;
		}
	}

	public class BashInputSpout
	{
		public Queue<KeyEvent> Input;

		public BashInputSpout()
		{
			this.Input = new Queue<KeyEvent>();
		}

		public void Push(KeyEvent keyEvent)
		{
			this.Input.Enqueue(keyEvent);
		}

		public KeyEvent Pull()
		{
			return this.Input.Dequeue();
		}

		public IEnumerable<KeyEvent> PullAll()
		{
			foreach (var keyEvent in this.Input)
			{
				yield return keyEvent;
			}
		}
	}

	public class BashController
	{
		private Bash bash;
		private BashView bashView;
		private BashInputSpout spout;

		public BashController(Bash bash, BashView bashView)
		{
			this.bash = bash;
			this.bashView = bashView;
			this.spout = bashView.Spout;
		}

		// When the view is letting us consume input.
		public void Tick()
		{
		}
	}

	// This is our view class,
	// it should primarily of public methods,
	// for the rendering of the model class to screen,
	// and of course,
	// the *private fields/methods providing the above*.
	// -
	// And, out of culture:,
	// it should also provide an InputSpout for the controller,
	// since the main window is often what gets the input,
	// (even though it's not what the view should touch).
	public class BashView
	{
		public BashInputSpout Spout;

		private Bash bash;

		private KeyboardState oks;
		private KeyboardState ks;

		public BashView()
		{
			this.bash = new Bash();
			this.Spout = new BashInputSpout();

			SadConsole.Game.Create("IBM.font", 80, 25);
			SadConsole.Game.OnInitialize = this.Init;
			SadConsole.Game.OnUpdate = this.Update;
			SadConsole.Game.Instance.Run();
		}

		public void Close()
		{
			SadConsole.Game.Instance.Exit();
		}
		
		private void Update(GameTime time)
		{
			// push to spout here.
			oks = ks;
			ks = Keyboard.GetState();

			foreach (var key in ks.GetPressedKeys())
			{
				if (oks != default(KeyboardState) && oks.GetPressedKeys().Contains(key))
				{
					// key down event
					this.Spout.Push(new KeyEvent(key, true));
				}
			}

			if (oks != default(KeyboardState))
			{
				foreach (var key in oks.GetPressedKeys())
				{
					if (!ks.GetPressedKeys().Contains(key))
					{
						// key up event
						this.Spout.Push(new KeyEvent(key, false));
					}
				}
			}

			// Called each logic update.

			// As an example, we'll use the F5 key to make the game full screen
			if (Global.KeyboardState.IsKeyReleased(Keys.F5))
			{
				Settings.ToggleFullScreen();
			}

			if (Global.KeyboardState.IsKeyReleased(Keys.Escape))
			{
				SadConsole.Game.Instance.Exit();
			}
		}

		private void Init()
		{
			// render here.

			// Any custom loading and prep. We will use a sample console for now
			SadConsole.Console startingConsole = new Console(80, 25);
			startingConsole.FillWithRandomGarbage();
			startingConsole.Fill(new Rectangle(3, 3, 27, 5), null, Color.Black, 0);
			startingConsole.Print(6, 5, "Hello from SadConsole", ColorAnsi.CyanBright);

			// Set our new console as the thing to render and process
			SadConsole.Global.CurrentScreen = startingConsole;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var view = new BashView();
		}
	}
}
