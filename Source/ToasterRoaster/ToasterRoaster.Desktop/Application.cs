using System;
using WaveEngine.Common.Input;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Desktop
{
	public class Application : WaveEngine.Adapter.Application
	{
		private Game.Game _game;

		public Application()
		{
			Width = 800;
			Height = 600;
			FullScreen = false;
			WindowTitle = "Toaster Roaster";
		}

		public override void Initialize()
		{
			_game = new Game.Game();
			_game.Initialize(this);
		}

		public override void Update(TimeSpan elapsedTime)
		{
			if ((_game != null)
				&& (!_game.HasExited))
			{
				if (WaveServices.Input.KeyboardState.Escape == ButtonState.Pressed)
				{
					WaveServices.Platform.Exit();
					return;
				}

				_game.UpdateFrame(elapsedTime);
			}
		}

		public override void Draw(TimeSpan elapsedTime)
		{
			if ((_game != null)
				&& (!_game.HasExited))
			{
				_game.DrawFrame(elapsedTime);
			}
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();

			_game.Suspend();
		}
	}
}