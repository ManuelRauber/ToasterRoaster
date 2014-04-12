using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
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

            WaveServices.GetService<LevelInformationService>().AddScore(WaveServices.GetService<LevelInformationService>().Level * accuracy);

            TextBlock accuracyText = new TextBlock("accuracyText")
            {
                Foreground = Color.Black,
                Text = "Die Übereinstimmung beträgt " + accuracy + "%.",
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

            var mainMenuButton = new Button()
            {
                Text = "Hauptmenü",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
                Width = 250,
            };
            mainMenuButton.Click += mainMenuButton_Click;
            EntityManager.Add(mainMenuButton);

            if (accuracy >= 80)
            {
                RenderManager.BackgroundColor = Color.Green;

                var nextLevelButton = new Button()
                {
                    Text = "Nächstes Level",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 100, 0, 0),
                    Width = 250,
                };
                nextLevelButton.Click += nextLevelButton_Click;

                EntityManager.Add(nextLevelButton);
            }
            else
            {
                RenderManager.BackgroundColor = Color.Red;

                var newGameButton = new Button()
                {
                    Text = "Neues Spiel",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 100, 0, 0),
                    Width = 250,
                };
                newGameButton.Click += newGameButton_Click;

                EntityManager.Add(newGameButton);
            }

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
