using System;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
	public class OptionsScene : Scene
	{
		private Grid _grid;
		private TextBox _playerNameBox;
		private Slider _musicVolumeSlider;
		private CheckBox _automaticHighscoreUploadCheckBox;

		protected override void CreateScene()
        {
            EntityManager.Add(CreateEntity.Background());

			CreateUI();

			WaveServices.GetService<AnalyticsService>().TagEvent("Page opened", "Page", "Options");
		}

		private void CreateUI()
		{
			CreateGrid();
			CreateHeader();
			CreatePlayerNameOption();
			CreateMusicVolumeOption();
			CreateAutomaticHighscoreUploadOption();
			CreateResetStatisticButton();
			CreateResetAchievementsButton();
			CreateButtons();
		}

		private void CreateResetStatisticButton()
		{
			var resetButton = new Button()
			{
				Text = "Reset statistic"
			};

			resetButton.Click += (sender, args) =>
			{
				var service = WaveServices.GetService<StatisticsService>();
				service.ResetStatistic();
				WaveServices.Platform.ShowMessageBox("Statistic reset", "Your statistics have been resetted");
			};

			resetButton.SetGridProperties(6, 0);
			resetButton.SetValue(GridControl.ColumnSpanProperty, 2);
			_grid.Add(resetButton);
		}

		private void CreateResetAchievementsButton()
		{
			var resetButton = new Button()
			{
				Text = "Reset achievements (debug)"
			};

			resetButton.Click += (sender, args) =>
			{
				var service = WaveServices.GetService<AchievementService>();
				service.Reset();
				WaveServices.Platform.ShowMessageBox("Achievements reset", "Your achievements have been resetted");
			};

			resetButton.SetGridProperties(7, 0);
			resetButton.SetValue(GridControl.ColumnSpanProperty, 2);
			_grid.Add(resetButton);
		}

		private void CreateButtons()
		{
			var stackPanel = new StackPanel()
			{
				Orientation = Orientation.Horizontal,
			};

			var okButton = new Button()
			{
				Text = "Save",
			};

			okButton.Click += OkButtonClicked;

			var cancelButton = new Button()
			{
				Text = "Cancel",
				Margin = new Thickness(10, 0, 0, 0),
			};

			cancelButton.Click += CancelButtonClicked;

			stackPanel.Add(okButton);
			stackPanel.Add(cancelButton);
			
			stackPanel.SetGridProperties(8, 0);
			_grid.Add(stackPanel);
		}

		private void CancelButtonClicked(object sender, EventArgs e)
		{
			SceneManager.Instance.To<MainMenuScene>();
		}

		private void OkButtonClicked(object sender, EventArgs e)
		{
			Configuration.Instance.MusicVolume = _musicVolumeSlider.Value;
			Configuration.Instance.PlayerName = _playerNameBox.Text;
			Configuration.Instance.AutomaticHighscoreUpload = _automaticHighscoreUploadCheckBox.IsChecked;

			WaveServices.Platform.ShowMessageBox("Settings saved", "Your settings have been saved!");
			SceneManager.Instance.To<MainMenuScene>();
		}

		private void CreateAutomaticHighscoreUploadOption()
		{
			_automaticHighscoreUploadCheckBox = new CheckBox()
			{
				Text = "Automatic highscore upload",
				IsChecked = Configuration.Instance.AutomaticHighscoreUpload,
			};

			_automaticHighscoreUploadCheckBox.SetGridProperties(5, 0);
			_grid.Add(_automaticHighscoreUploadCheckBox);
		}

		private void CreateMusicVolumeOption()
		{
			var textBlock = new TextBlock()
			{
				Text = "Music volume:"
			};

			textBlock.SetGridProperties(3, 0);
			
			_musicVolumeSlider = new Slider()
			{
				Minimum = 0,
				Maximum = 100,
				Value = Configuration.Instance.MusicVolume,
				Width = 200,
			};

			_musicVolumeSlider.SetGridProperties(4, 0);

			_grid.Add(textBlock);
			_grid.Add(_musicVolumeSlider);
		}

		private void CreatePlayerNameOption()
		{
			var textBlock = new TextBlock()
			{
				Text = "Player name:",
			};

			textBlock.SetGridProperties(1, 0);

			_playerNameBox = new TextBox()
			{
				Width = 200,
				Text = Configuration.Instance.PlayerName
			};

			_playerNameBox.SetGridProperties(2, 0);

			_grid.Add(textBlock);
			_grid.Add(_playerNameBox);
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
			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
			_grid.ColumnDefinitions.Add(new ColumnDefinition());

			EntityManager.Add(_grid);
		}

		private void CreateHeader()
		{
			var textBlock = new TextBlock()
			{
				Text = "ToasterRoaster - Optionen",
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			textBlock.SetGridProperties(0, 0);

			_grid.Add(textBlock);
		}
	}
}