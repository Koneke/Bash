using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bash
{
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

		private SadConsole.Console console;
		private KeyboardState oks;
		private KeyboardState ks;

		public BashView()
		{
			this.Spout = new BashInputSpout();
			this.bash = new Bash();

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

			if (Global.KeyboardState.IsKeyReleased(Keys.Escape))
			{
				SadConsole.Game.Instance.Exit();
			}
		}

		private void Init()
		{
			this.console = new SadConsole.Console(80, 25);

			// render here.
			console.Fill(Color.White, Color.Black, null);

			// Any custom loading and prep. We will use a sample console for now
			SadConsole.Console startingConsole = new Console(80, 25);
			startingConsole.FillWithRandomGarbage();
			startingConsole.Fill(new Rectangle(3, 3, 27, 5), null, Color.Black, 0);
			startingConsole.Print(6, 5, "Hello from SadConsole", ColorAnsi.CyanBright);

			// Set our new console as the thing to render and process
			//SadConsole.Global.CurrentScreen = startingConsole;

			SadConsole.Global.CurrentScreen = console;
		}
	}
}
