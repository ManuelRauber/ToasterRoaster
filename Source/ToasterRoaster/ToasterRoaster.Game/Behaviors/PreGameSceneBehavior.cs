using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var textBlock = (this.Scene as PreGameScene).EntityManager.Find<TextBlock>("Timer");
            textBlock.IsVisible = true;
            textBlock.Text = "Spiel startet in " + _time.ToString() + " Sekunden.";

            if (_timer == null)
            {
                _timer = WaveServices.TimerFactory.CreateTimer("Countdown", TimeSpan.FromSeconds(1f), () =>
                {
                    textBlock.Text = "Spiel startet in " + _time.ToString() + " Sekunden.";
                    _time--;
                });
            }

            if (_time <= 0)
            {
                WaveServices.TimerFactory.RemoveTimer("Init");
                SceneManager.Instance.To<GameScene>();
                this.IsActive = false;
            }
        }

        protected override void ResolveDependencies()
        {
        }
    }
}
