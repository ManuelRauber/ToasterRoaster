using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Behaviors;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using ToasterRoaster.Game.Services.Achievements;
using ToasterRoaster.Game.Services.Highscore;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
    class EvaluationScene : Scene
    {
        protected override void CreateScene()
        {
            BoardComparer boardComparer = new BoardComparer();
            bool[,] givenTexture = WaveServices.GetService<TextureMapService>().GivenTexture;
            bool[,] drawnTexture = WaveServices.GetService<TextureMapService>().DrawnTexture;
            double accuracy = boardComparer.GetAccuracyInPercent(givenTexture, drawnTexture);

	        var levelService = WaveServices.GetService<LevelInformationService>();
	        var statisticsService = WaveServices.GetService<StatisticsService>();
	        var achievementService = WaveServices.GetService<AchievementService>();

	        statisticsService.OverallAccuracy += accuracy;

            levelService.AddScore(levelService.Level * accuracy);
            
            TextBlock levelText = new TextBlock("levelText")
            {
                Foreground = Color.Black,
                Text = "Level " + WaveServices.GetService<LevelInformationService>().Level,
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            EntityManager.Add(levelText);
            
            TextBlock accuracyText = new TextBlock("accuracyText")
            {
                Foreground = Color.Black,
                Text = String.Format("{0}, Deine Übereinstimmung beträgt {1:##.##}%.", Configuration.Instance.PlayerName, accuracy),
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            EntityManager.Add(accuracyText);

            TextBlock scoreText = new TextBlock("scoreText")
            {
                Foreground = Color.Black,
                Text = "Punkte: " + WaveServices.GetService<LevelInformationService>().Score,
                Margin = new Thickness(50f),
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            EntityManager.Add(scoreText);

            CreateTexturedToast(givenTexture, "given", "Vorlage", new Vector2(WaveServices.Platform.ScreenWidth / 2 - 100, 200));
            CreateTexturedToast(drawnTexture, "drawn", "gemaltes Muster", new Vector2(WaveServices.Platform.ScreenWidth / 2 + 100, 200));

            var mainMenuButton = new Button()
            {
                Text = "Hauptmenü",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 100, 0, 0),
                Width = 250,
            };
            mainMenuButton.Click += mainMenuButton_Click;
            EntityManager.Add(mainMenuButton);

						achievementService.Steps(GameStep.GameFinished);

            if (accuracy >= 80)
            {
	            statisticsService.GamesWon++;
							achievementService.Steps(GameStep.GameWon);

	            if (accuracy >= 99.9)
	            {
		            achievementService.Steps(GameStep.OneHundredPercentAccuracy);
	            }

	            if (accuracy >= 95)
	            {
		            statisticsService.ThreeStarGames++;
								achievementService.Steps(GameStep.ThreeStarGameWon);
	            }
							else if (accuracy >= 88)
							{
								statisticsService.TwoStarGames++;
								achievementService.Steps(GameStep.TwoStarGameWon);
							}
							else
							{
								statisticsService.OneStarGames++;
								achievementService.Steps(GameStep.OneStarGameWon);
							}

                RenderManager.BackgroundColor = Color.Green;

                var nextLevelButton = new Button()
                {
                    Text = "Nächstes Level",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 250,
                };
                nextLevelButton.Click += nextLevelButton_Click;

                EntityManager.Add(nextLevelButton);
            }
            else
            {
	            if (accuracy <= 0.1)
	            {
		            achievementService.Steps(GameStep.ZeroPercentAccuracy);
	            }

	            statisticsService.GamesLost++;
							achievementService.Steps(GameStep.GameLost);
                RenderManager.BackgroundColor = Color.Red;

                var newGameButton = new Button()
                {
                    Text = "Neues Spiel",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 250,
                };
                newGameButton.Click += newGameButton_Click;

                EntityManager.Add(newGameButton);

							WaveServices.GetService<HighscoreServices>().AddHighscore(new HighscoreEntry()
							{
								Level = Convert.ToInt32(levelService.Level),
								Points = Convert.ToInt32(levelService.Score),
								Name = Configuration.Instance.PlayerName,
							});
            }

			AddSceneBehavior(new EvaluationSceneBehavior(givenTexture, drawnTexture), SceneBehavior.Order.PostUpdate);

			WaveServices.GetService<AnalyticsService>().TagEvent("Page opened", "Page", "Evaluation");
        }

        private void CreateTexturedToast(bool[,] texture, string name, string text, Vector2 position)
        {
            bool[,] scaledTexture = BoolToTextureConverter.ScaleTexture(texture, 100, 100);

            TextBlock textBlock = new TextBlock(name + "Text")
            {
                Foreground = Color.Black,
                Text = text,
                Margin = new Thickness(position.X - 50, position.Y - 75, 0, 0),
            };
            EntityManager.Add(textBlock);
            
            Entity previewToast = new Entity(name + "Toast")
                .AddComponent(new Sprite("Assets/Textures/toast_2D.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = position.X,
                    Y = position.Y,
                    DrawOrder = 0.1f,
                    XScale = 0.2f,
                    YScale = 0.2f,
                    Origin = Vector2.Center,
                });
            EntityManager.Add(previewToast);

            Entity previewModel = new Entity(name + "Texture")
                .AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(scaledTexture, this.RenderManager)))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = position.X,
                    Y = position.Y,
                    DrawOrder = 0.05f,
                    Origin = Vector2.Center,
                });
            EntityManager.Add(previewModel);
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            SceneManager.Instance.To<MainMenuScene>();
        }

        private void nextLevelButton_Click(object sender, EventArgs e)
        {
            WaveServices.GetService<LevelInformationService>().IncreaseLevel();
            SceneManager.Instance.To<PreGameScene>();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            WaveServices.GetService<LevelInformationService>().Reset();
            SceneManager.Instance.To<PreGameScene>();
        }
    }
}
