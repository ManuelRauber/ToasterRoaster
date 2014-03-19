using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using WaveEngine.Common.Input;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Android
{
  [Activity(
    Label = "ToasterRoaster.Android",
    MainLauncher = true,
    Icon = "@drawable/icon",
    LaunchMode = LaunchMode.SingleTask,
    ConfigurationChanges = ConfigChanges.ScreenLayout | ConfigChanges.Orientation
    )]
  public class App : WaveEngine.Adapter.Application
  {
    private Game.Game _game;

    public App()
    {
      FullScreen = true;
      DefaultOrientation = DisplayOrientation.Portrait;
      SupportedOrientations = DisplayOrientation.Portrait;
    }

    public override void Initialize()
    {
      _game = new Game.Game();
      _game.Initialize(this);

      Window.AddFlags(WindowManagerFlags.KeepScreenOn);
    }

    public override void Update(TimeSpan elapsedTime)
    {
      _game.UpdateFrame(elapsedTime);
    }

    public override void Draw(TimeSpan elapsedTime)
    {
      _game.DrawFrame(elapsedTime);
    }

    public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
    {
      if (Keycode.Back == keyCode)
      {
        WaveServices.Platform.Exit();
      }

      return base.OnKeyDown(keyCode, e);
    }
  }
}