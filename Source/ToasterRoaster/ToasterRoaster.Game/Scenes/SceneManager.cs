using System;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Scenes
{
  public class SceneManager
  {
		private readonly ScreenContextManager _layers;

    private static readonly Lazy<SceneManager> _instance = new Lazy<SceneManager>(() => new SceneManager());

    public static SceneManager Instance
    {
      get { return _instance.Value; }
    }

    private SceneManager()
    {
      _layers = WaveServices.ScreenContextManager;
    }

	  public void ChangeSceneTo<TScene>()
		  where TScene : Scene, new()
	  {
		  ChangeSceneTo(new TScene());
	  }

    private void ChangeSceneTo(Scene scene)
    {
			_layers.To(new ScreenContext(scene));
		}
  }
}