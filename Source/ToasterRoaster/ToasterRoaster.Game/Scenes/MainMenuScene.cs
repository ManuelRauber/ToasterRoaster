using System;
using System.Threading;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Animation;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
  public class MainMenuScene : Scene
  {
    private Grid _grid;

    protected override void CreateScene()
    {
      //RenderManager.DebugLines = true;
      CreateUI();
    }

    private void CreateUI()
    {
      CreateGrid();
      CreateHeader();
      CreateGameStartButton();
      CreateStatisticsButton();
      CreateHighscoreButton();
      CreateHelpButton();
      CreateOptionsButton();
    }

    private void CreateGrid()
    {
      _grid = new Grid()
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Stretch,
      };

      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
      _grid.ColumnDefinitions.Add(new ColumnDefinition());

      EntityManager.Add(_grid);
    }

    private void CreateHeader()
    {
      var textBlock = new TextBlock()
      {
        Text = "ToasterRoaster",
        HorizontalAlignment = HorizontalAlignment.Center,
      };

      textBlock.SetValue(GridControl.RowProperty, 0);
      textBlock.SetValue(GridControl.ColumnProperty, 0);
      _grid.Add(textBlock);
    }

    private void CreateGameStartButton()
    {
      var button = CreateSimpleButton("Start", 1, GameStartButtonClicked);
      button.Margin = new Thickness(0, 40, 0, 0);
    }

    private void CreateStatisticsButton()
    {
      CreateSimpleButton("Statistics", 2, StatisticsButtonClicked);
    }

    private void CreateHighscoreButton()
    {
      CreateSimpleButton("Highscore", 3, HighscoreButtonClicked);
    }

    private void CreateHelpButton()
    {
      CreateSimpleButton("Help", 4, HelpButtonClicked);
    }

    private void CreateOptionsButton()
    {
      CreateSimpleButton("Options", 5, OptionsButtonClicked);
    }

    private Button CreateSimpleButton(string text, int row, EventHandler clickHandler)
    {
      var button = new Button()
      {
        Text = text,
        HorizontalAlignment = HorizontalAlignment.Center,
        Width = 250,
      };

      button.SetValue(GridControl.ColumnProperty, 0);
      button.SetValue(GridControl.RowProperty, row);

      button.Click += clickHandler;

      _grid.Add(button);

      return button;
    }

    #region Events

    private void OptionsButtonClicked(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void HighscoreButtonClicked(object sender, EventArgs eventArgs)
    {
      throw new NotImplementedException();
    }

    private void StatisticsButtonClicked(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void GameStartButtonClicked(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void HelpButtonClicked(object sender, EventArgs e)
    {
      SceneManager.Instance.ChangeSceneTo<HelpScene>();
    }

    #endregion
  }
}