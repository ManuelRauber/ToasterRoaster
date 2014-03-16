using System;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
  public class HelpScene : Scene
  {
    private Grid _grid;

    protected override void CreateScene()
    {
      CreateGrid();
      CreateHeader();
      CreateHelpText();
      CreateBackButton();
    }

    private void CreateBackButton()
    {
      var button = new Button()
      {
        Text = "Back to main menu",
        Margin = new Thickness(0, 40, 0, 0),
      };

      button.SetValue(GridControl.ColumnProperty, 0);
      button.SetValue(GridControl.RowProperty, 2);

      button.Click += BackButtonClicked;

      _grid.Add(button);
    }

    private void BackButtonClicked(object sender, EventArgs e)
    {
      SceneManager.Instance.ChangeSceneTo<MainMenuScene>();
    }

    private void CreateHelpText()
    {
      var textBlock = new TextBlock()
      {
        TextWrapping = false,
        Margin = new Thickness(0, 40, 0, 0),
        Text = "Helptext not defined right now. I'm just a placeholder. :-)",
        HorizontalAlignment = HorizontalAlignment.Left,
      };

      textBlock.SetValue(GridControl.ColumnProperty, 0);
      textBlock.SetValue(GridControl.RowProperty, 1);

      _grid.Add(textBlock);
    }

    private void CreateHeader()
    {
      var textBlock = new TextBlock()
      {
        Text = "ToasterRoaster - Help",
        HorizontalAlignment = HorizontalAlignment.Center,
      };

      textBlock.SetValue(GridControl.ColumnProperty, 0);
      textBlock.SetValue(GridControl.RowProperty, 0);

      _grid.Add(textBlock);
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
      _grid.ColumnDefinitions.Add(new ColumnDefinition());

      EntityManager.Add(_grid);
    }
  }
}