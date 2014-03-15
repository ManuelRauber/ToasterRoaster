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

      SceneManager.Instance.ChangeSceneTo<MainMenuScene>();
    }
  }
}