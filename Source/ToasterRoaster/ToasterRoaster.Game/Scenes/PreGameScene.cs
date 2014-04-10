using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Behaviors;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;

namespace ToasterRoaster.Game.Scenes
{
    class PreGameScene : Scene
    {
        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.Red;

            TextBlock infoText = new TextBlock("infoText")
            {
                Foreground = Color.Black,
                Text = "Malen Sie diese Figur:",
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            EntityManager.Add(infoText);

            //Add Picture here
            WaveServices.GetService<TextureMapService>().GivenTexture = new bool[280, 280];

            float size = 280;
            Entity previewModel = new Entity("previewModel")
                .AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(new bool[280, 280], this.RenderManager)))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = (WaveServices.Platform.ScreenWidth - size) / 2,
                    Y = (WaveServices.Platform.ScreenHeight - size) / 2,
                    DrawOrder = 0.05f,
                });


            TextBlock timerText = new TextBlock("Timer")
            {
                Foreground = Color.Black,
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            EntityManager.Add(timerText);

            this.AddSceneBehavior(new PreGameSceneBehavior(), SceneBehavior.Order.PostUpdate);
        }
    }
}
