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
            RenderManager.BackgroundColor = Color.Red;

            BoardComparer boardComparer = new BoardComparer();
            bool[,] givenTexture = WaveServices.GetService<TextureMapService>().GivenTexture;
            bool[,] drawnTexture = WaveServices.GetService<TextureMapService>().DrawnTexture;

            

            TextBlock accuracity = new TextBlock("accuracity")
            {
                Foreground = Color.Black,
                Text = "Die Übereinstimmung beträgt " + boardComparer.GetAccuracyInPercent(givenTexture, drawnTexture) + "%.",
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            EntityManager.Add(accuracity);

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

        void mainMenuButton_Click(object sender, EventArgs e)
        {
            SceneManager.Instance.To<MainMenuScene>();
        }

        void nextLevelButton_Click(object sender, EventArgs e)
        {
            SceneManager.Instance.To<PreGameScene>();
        }
    }
}
