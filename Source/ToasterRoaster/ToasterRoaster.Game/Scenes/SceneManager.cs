using System;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Scenes
{
  public class SceneManager
  {
    private readonly ScreenLayers _layers;

    private static readonly Lazy<SceneManager> _instance = new Lazy<SceneManager>(() => new SceneManager());

    public static SceneManager Instance
    {
      get { return _instance.Value; }
    }

    private SceneManager()
    {
      _layers = WaveServices.ScreenLayers;
    }

    public void ChangeSceneTo<TScene>()
      where TScene : Scene, new()
    {
      _layers.AddScene<TScene>();
      _layers.Apply();
    }
  }
}