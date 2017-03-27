using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bash
{
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
}
