using System;
using ToasterRoaster.Game.Scenes;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Behaviors
{
    class PreGameSceneBehavior : SceneBehavior
    {
        private Timer _timer;
        private int _time = 3;
        protected override void Update(TimeSpan gameTime)
        {
            var textBlock = Scene.EntityManager.Find<TextBlock>("Timer");
            textBlock.IsVisible = true;
            textBlock.Text = String.Format("Spiel startet in {0} Sekunden.", _time);

            if (_timer == null)
            {
                _timer = WaveServices.TimerFactory.CreateTimer("Countdown", TimeSpan.FromSeconds(1f), () =>
                {
	                if (_time < 0) return;

                    textBlock.Text = String.Format("Spiel startet in {0} Sekunden.", _time);
                    _time--;
                });
            }

	        if (_time > 0) return;
	        WaveServices.TimerFactory.RemoveTimer("Init");
	        SceneManager.Instance.To<GameScene>();
	        IsActive = false;
        }

        protected override void ResolveDependencies()
        {
        }
    }
}
