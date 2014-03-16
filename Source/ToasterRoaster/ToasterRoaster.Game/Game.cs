using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Scenes;
using WaveEngine.Common;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game
{
  public class Game : WaveEngine.Framework.Game
  {
    public override void Initialize(IApplication application)
    {
      base.Initialize(application);

      BackgroundMusicPlayer.Instance.Start();
      SceneManager.Instance.ChangeSceneTo<MainMenuScene>();
    }
  }
}