using System;
using System.Collections.Generic;
using System.Linq;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using ToasterRoaster.Game.Services.Achievements;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
	public class AchievementsScene : Scene
	{
		private Grid _grid;
		private List<IAchievement> _achievements; 


		protected override void CreateScene()
		{
			_achievements = WaveServices.GetService<AchievementService>().Achievements;

			CreateUI();
		}

		private void CreateUI()
		{
			CreateGrid();
			CreateHeader();
			CreateAchievements();
			CreateBackButton();
		}

		private void CreateAchievements()
		{
			int count = 0;
			foreach (var achievement in _achievements.OrderByDescending(x => x.PercentageCompleted >= 100).ThenBy(x => x.Title))
			{
				CreateAchievement(achievement, 1 + count);
				count++;
			}
		}

		private void CreateAchievement(IAchievement achievement, int row)
		{
			var titleText = new TextBlock()
			{
				Text = String.Format("{0}\r\n({1})", achievement.Title, achievement.Description),
				TextAlignment = TextAlignment.Left,
				HorizontalAlignment = HorizontalAlignment.Left,
				Height = 55
			};

			titleText.SetGridProperties(row, 0);

			var completedText = new TextBlock()
			{
				Text = achievement.PercentageCompleted >= 100 ? "Completed" : "Not completed",
				TextAlignment = TextAlignment.Right,
				HorizontalAlignment = HorizontalAlignment.Right,
				Width = 200
			};

			completedText.SetGridProperties(row, 1);

			_grid.Add(titleText);
			_grid.Add(completedText);
		}

		private void CreateGrid()
		{
			_grid = new Grid()
			{
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Stretch,
				Width = WaveServices.Platform.ScreenWidth,
			};

			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });

			for (int i = 0; i < _achievements.Count; i++)
			{
				_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			}

			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Proportional) });

			EntityManager.Add(_grid);
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