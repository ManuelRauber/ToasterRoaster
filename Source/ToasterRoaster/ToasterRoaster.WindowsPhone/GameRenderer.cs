using System;
using System.Windows.Controls;
using Windows.Foundation;
using WaveEngine.Adapter;
using WaveEngine.Common.Input;

namespace ToasterRoaster.WindowsPhone
{
	public class GameRenderer : Application
	{
		private Game.Game _game;

		public GameRenderer(Size windowBounds, int scaleFactor, MediaElement mediaElement)
			: base(windowBounds, scaleFactor, mediaElement)
		{ }

		public override void Update(TimeSpan elapsedTime)
		{
			_game.UpdateFrame(elapsedTime);
		}

		public override void Draw(TimeSpan elapsedTime)
		{
			_game.DrawFrame(elapsedTime);
		}

		public override void OnSuspending()
		{
			_game.Suspend();
		}

		public override void OnResuming()
		{ }

		public override void Initialize()
		{
			base.Initialize();

			Adapter.DefaultOrientation = DisplayOrientation.Portrait;
			Adapter.SupportedOrientations = DisplayOrientation.Portrait;

			_game = new Game.Game();
			_game.Initialize(this);
		}
	}
}