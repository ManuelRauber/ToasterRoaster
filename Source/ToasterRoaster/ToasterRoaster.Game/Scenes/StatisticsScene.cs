using System.Collections.Generic;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
	public class StatisticsScene : Scene
	{
		private Grid _grid;

		private Dictionary<string, object> _statisticMap;

		protected override void CreateScene()
		{
			CreateStatisticMap();
			CreateUI();
		}

		private void CreateStatisticMap()
		{
			var service = WaveServices.GetService<StatisticsService>();

			_statisticMap = new Dictionary<string, object>()
			{
				{ "Total Games played", service.TotalGamesPlayed },
				{ "Overall accuracy", service.OverallAccuracy },
				{ "Games won", service.GamesWon },
				{ "Games lost", service.GamesLost },
				{ "Three-star games", service.ThreeStarGames },
				{ "Two-star games", service.TwoStarGames },
				{ "One-star games", service.OneStarGames },
			};
		}

		private void CreateUI()
		{
			CreateGrid();
			CreateHeader();
			CreateStatistics();
			CreateBackButton();
		}

		private void CreateBackButton()
		{
			var backButton = new Button()
			{
				Text = "Back",
			};

			backButton.Click += (sender, args) => SceneManager.Instance.To<MainMenuScene>();

			backButton.SetGridProperties(_grid.RowDefinitions.Count - 1, 0);
			backButton.SetValue(GridControl.ColumnSpanProperty, 2);
			_grid.Add(backButton);
		}


		private void CreateStatistics()
		{
			int count = 0;
			foreach (KeyValuePair<string, object> kvp in _statisticMap)
			{
				CreateStatistic(kvp.Key, kvp.Value, 1 + count);
				count++;
			}
		}

		private void CreateStatistic(string statisticName, object value, int row)
		{
			var nameBlock = new TextBlock()
			{
				Text = statisticName,
				TextAlignment = TextAlignment.Left,
				HorizontalAlignment = HorizontalAlignment.Left,
			};

			nameBlock.SetGridProperties(row, 0);

			_grid.Add(nameBlock);

			var valueBlock = new TextBlock()
			{
				Text = value.ToString(),
				TextAlignment = TextAlignment.Right,
				HorizontalAlignment = HorizontalAlignment.Right,
			};

			valueBlock.SetGridProperties(row, 1);

			_grid.Add(valueBlock);
		}

		private void CreateGrid()
		{
			_grid = new Grid()
			{
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Stretch,
				Width = 500,
			};

			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

			for (int i = 0; i < _statisticMap.Count; i++)
			{
				_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			}

			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });

			EntityManager.Add(_grid);
		}

		private void CreateHeader()
		{
			var textBlock = new TextBlock()
			{
				Text = "ToasterRoaster - Statistics",
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			textBlock.SetValue(GridControl.ColumnSpanProperty, 2);
			textBlock.SetGridProperties(0, 0);

			_grid.Add(textBlock);
		}
	}
}