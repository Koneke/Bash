namespace Bash
{
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
}
