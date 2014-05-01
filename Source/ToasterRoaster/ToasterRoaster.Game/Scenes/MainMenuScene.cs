using System;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
	public class MainMenuScene : Scene
	{
		private Grid _grid;

		protected override void CreateScene()
		{
			CreateUI();

			WaveServices.GetService<AnalyticsService>().TagEvent("Page opened", "Page", "Main Menu");
		}

		// ReSharper disable once InconsistentNaming
		private void CreateUI()
		{
			CreateGrid();
			CreateHeader();
			CreateGameStartButton();
			CreateStatisticsButton();
			CreateAchievementButton();
			CreateHighscoreButton();
			CreateHelpButton();
			CreateOptionsButton();
		}

		private void CreateGrid()
		{
			_grid = new Grid
			{
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Stretch,
			};

			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition());

			EntityManager.Add(_grid);
		}

		private void CreateHeader()
		{
			var textBlock = new TextBlock
			{
				Text = "ToasterRoaster",
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			textBlock.SetGridProperties(0, 0);

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

		private void CreateAchievementButton()
		{
			CreateSimpleButton("Achievements", 3, AchievementButtonClicked);
		}

		private void CreateHighscoreButton()
		{
			CreateSimpleButton("Highscore", 4, HighscoreButtonClicked);
		}

		private void CreateHelpButton()
		{
			CreateSimpleButton("Help", 5, HelpButtonClicked);
		}

		private void CreateOptionsButton()
		{
			CreateSimpleButton("Options", 6, OptionsButtonClicked);
		}

		private Button CreateSimpleButton(string text, int row, EventHandler clickHandler)
		{
			var button = new Button
			{
				Text = text,
				HorizontalAlignment = HorizontalAlignment.Center,
				Width = 250,
			};

			button.SetGridProperties(row, 0);

			button.Entity.RemoveComponent<BorderRenderer>();

			button.Click += clickHandler;

			_grid.Add(button);

			return button;
		}

		#region Events

		private static void OptionsButtonClicked(object sender, EventArgs e)
		{
			SceneManager.Instance.To<OptionsScene>();
		}

		private static void HighscoreButtonClicked(object sender, EventArgs eventArgs)
		{
			SceneManager.Instance.To<HighscoreScene>();
		}

		private static void StatisticsButtonClicked(object sender, EventArgs e)
		{
			SceneManager.Instance.To<StatisticsScene>();
		}

		private static void GameStartButtonClicked(object sender, EventArgs e)
		{
			WaveServices.GetService<LevelInformationService>().Reset();
			SceneManager.Instance.To<PreGameScene>();
		}

		private static void HelpButtonClicked(object sender, EventArgs e)
		{
			SceneManager.Instance.To<HelpScene>();
		}

		private void AchievementButtonClicked(object sender, EventArgs e)
		{
			SceneManager.Instance.To<AchievementsScene>();
		}

		#endregion
	}
}