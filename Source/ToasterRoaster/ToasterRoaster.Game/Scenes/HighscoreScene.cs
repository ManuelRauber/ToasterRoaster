using System;
using System.Collections.Generic;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services.Highscore;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
	public class HighscoreScene : Scene
	{
		private Grid _grid;

		protected override void CreateScene()
		{
			CreateUI();
		}

		private void CreateUI()
		{
			CreateGrid();
			CreateHeader();
			CreateLocalHighscoreTable();
			CreateBackButton();
		}

		private void CreateBackButton()
		{
			var button = new Button()
			{
				Text = "Back",
			};

			button.SetGridProperties(2, 0);
			button.Click += (sender, args) => SceneManager.Instance.To<MainMenuScene>();
			_grid.Add(button);
		}

		private void CreateLocalHighscoreTable()
		{
			var stackPanel = new StackPanel();
			stackPanel.SetGridProperties(1, 0);

			var header = new TextBlock()
			{
				Text = "Local",
			};

			stackPanel.Add(header);

			var items = HighscoreManager.Instance.GetTopTenFrom("local");

			AddHighscoreEntries(items, stackPanel);

			_grid.Add(stackPanel);
		}

		private void AddHighscoreEntries(IEnumerable<HighscoreEntry> items, StackPanel panelToInsert)
		{
			foreach (var item in items)
			{
				var entry = new TextBlock()
				{
					Text = String.Format("{0}: {1} ({2})", item.Name, item.Points, item.Level),
				};

				panelToInsert.Add(entry);
			}
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
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });

			EntityManager.Add(_grid);
		}

		private void CreateHeader()
		{
			var textBlock = new TextBlock()
			{
				Text = "ToasterRoaster - Optionen",
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			textBlock.SetValue(GridControl.ColumnSpanProperty, 2);
			textBlock.SetGridProperties(0, 0);

			_grid.Add(textBlock);
		}
	}
}