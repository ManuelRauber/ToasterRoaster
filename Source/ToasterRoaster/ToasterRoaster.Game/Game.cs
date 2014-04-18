using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Scenes;
using ToasterRoaster.Game.Services;
using ToasterRoaster.Game.Services.Highscore;
using WaveEngine.Common;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game
{
  public class Game : WaveEngine.Framework.Game
  {
    public override void Initialize(IApplication application)
    {
      base.Initialize(application);

      WaveServices.RegisterService<TextureMapService>(new TextureMapService());
      WaveServices.RegisterService<LevelInformationService>(new LevelInformationService());

			HighscoreManager.Instance.RegisterDefaultServices();

      BackgroundMusicPlayer.Instance.Start();
      SceneManager.Instance.To<MainMenuScene>();
    }
  }
}