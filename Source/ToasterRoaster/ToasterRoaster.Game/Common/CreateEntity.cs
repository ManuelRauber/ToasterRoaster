using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Common
{
    public static class CreateEntity
    {
        public static Entity Background()
        {
            var background = new Entity("Background")
                            .AddComponent(new Sprite("Assets/Background/BackgroundMenu.wpk"))
                            .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                            .AddComponent(new Transform2D()
                            {
                                XScale = WaveServices.Platform.ScreenWidth / 800,
                                YScale = WaveServices.Platform.ScreenHeight / 600,
                            });

            return background;
        }
    }
}
