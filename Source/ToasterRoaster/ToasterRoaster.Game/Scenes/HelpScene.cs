using System;
using System.Linq;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
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
	  //  RenderManager.DebugLines = true;

      CreateGrid();
      CreateHeader();
      CreateHelpText();
	  CreateBackButton();

	  WaveServices.GetService<AnalyticsService>().TagEvent("Page opened", "Page", "Help");
    }

    private void CreateBackButton()
    {
      var button = new Button()
      {
        Text = "Back to main menu",
        Margin = new Thickness(0, 40, 0, 0),
				Width = 250,
				HorizontalAlignment = HorizontalAlignment.Center,
      };

      button.SetGridProperties(2, 0);

      button.Click += BackButtonClicked;

      _grid.Add(button);
    }

    private void BackButtonClicked(object sender, EventArgs e)
    {
      SceneManager.Instance.To<MainMenuScene>();
    }

    private void CreateHelpText()
    {
      var textBlock = new TextBlock()
      {
        TextWrapping = true,
        Margin = new Thickness(0, 40, 0, 0),
        Text = "Helptext not defined right now. I'm just a placeholder. :-)",
				TextAlignment = TextAlignment.Left,
				Width = 500,
      };

      textBlock.SetGridProperties(1, 0);

      _grid.Add(textBlock);
    }

    private void CreateHeader()
    {
      var textBlock = new TextBlock()
      {
        Text = "ToasterRoaster - Help",
				TextAlignment = TextAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center,
      };

      textBlock.SetGridProperties(0, 0);

      _grid.Add(textBlock);
    }

    private void CreateGrid()
    {
      _grid = new Grid()
      {
				Width = WaveServices.Platform.ScreenWidth, 
				HorizontalAlignment = HorizontalAlignment.Center,
      };

      _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Proportional) });
			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Proportional) });
			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Proportional) });
      _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });

      EntityManager.Add(_grid);
    }
  }
}