using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Scenes;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
using ToasterRoaster.Game.Behaviors;
using WaveEngine.Common.Math;

namespace ToasterRoaster.Game.Scenes
{
    class PreGameScene : Scene
    {
        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.YellowGreen;

            TextBlock levelText = new TextBlock("levelText")
            {
                Foreground = Color.Black,
                Text = "Level " + WaveServices.GetService<LevelInformationService>().Level,
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            EntityManager.Add(levelText);

            TextBlock infoText = new TextBlock("infoText")
            {
                Foreground = Color.Black,
                Text = "Malen Sie dieses Muster:",
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            EntityManager.Add(infoText);

            //Add Picture here
            //bool[,] texture = new bool[,]
            //    {
            //        {true, false, false, false, true},
            //        {false, false, true, false, false},
            //        {false, true, false, true, false},
            //        {false, false, true, false, false},
            //        {true, false, false, false, true},
            //    };

            ulong textureSize = WaveServices.GetService<LevelInformationService>().TextureSize;
            bool[,] texture = new bool[textureSize, textureSize];
            for (ulong i = 0; i < textureSize; i++)
            {
                for (ulong j = 0; j < textureSize; j++)
                {
                    texture[i, j] = WaveServices.Random.NextBool();
                }
            }

            WaveServices.GetService<TextureMapService>().GivenTexture = texture;

            bool[,] scaledTexture = BoolToTextureConverter.ScaleTexture(texture, 100, 100);

            Entity previewToast = new Entity("previewToast")
                .AddComponent(new Sprite("Assets/Textures/toast_2D.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = WaveServices.Platform.ScreenHeight / 2,
                    DrawOrder = 0.1f,
                    XScale = 0.2f,
                    YScale = 0.2f,
                    Origin = Vector2.Center,
                });
            EntityManager.Add(previewToast);

            Entity previewTexture = new Entity("previewTexture")
                .AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(scaledTexture, this.RenderManager)))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = WaveServices.Platform.ScreenHeight / 2,
                    DrawOrder = 0.05f,
                    Origin = Vector2.Center,
                });
            EntityManager.Add(previewTexture);


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
