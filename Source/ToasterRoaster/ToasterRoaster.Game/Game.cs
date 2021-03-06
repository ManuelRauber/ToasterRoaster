﻿using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Scenes;
using ToasterRoaster.Game.Services;
using ToasterRoaster.Game.Services.Highscore;
using WaveEngine.Common;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.Sound;

namespace ToasterRoaster.Game
{
	public class Game : WaveEngine.Framework.Game
	{
		public override void Initialize(IApplication application)
		{
			base.Initialize(application);

			WaveServices.RegisterService(new TextureMapService());
			WaveServices.RegisterService(new LevelInformationService());
			WaveServices.RegisterService(new AnalyticsService(application));
			WaveServices.RegisterService(new HighscoreServices());
			WaveServices.RegisterService(new StatisticsService());
			WaveServices.RegisterService(new AchievementService());
            WaveServices.RegisterService(new SoundService());
			WaveServices.RegisterService(new AchievementDispatcher());
			
			WaveServices.GetService<HighscoreServices>().RegisterDefaultServices();

			BackgroundMusicPlayer.Instance.Start();
			SceneManager.Instance.To<MainMenuScene>();
		}

		public void Suspend()
		{
			WaveServices.GetService<AnalyticsService>().Close();
		}
	}
}