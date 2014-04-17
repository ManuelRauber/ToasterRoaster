using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Common;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Behaviors
{
    class EvaluationSceneBehavior : SceneBehavior
    {
        private bool[,] _givenTexture;
        private Entity _givenToastEntity;
        private Entity _givenTextureEntity;

        private bool[,] _drawnTexture;
        private Entity _drawnToastEntity;
        private Entity _drawnTextureEntity;

        private Entity _centeredToast;
        private Entity _centeredTexture;

        public EvaluationSceneBehavior(bool[,] givenTexture, bool[,] drawnTexture)
        {
            _givenTexture = givenTexture;
            _drawnTexture = drawnTexture;
        }

        protected override void ResolveDependencies()
        {
            _givenToastEntity = Scene.EntityManager.Find("givenToast");
            _givenTextureEntity = Scene.EntityManager.Find("givenTexture");

            _drawnToastEntity = Scene.EntityManager.Find("drawnToast");
            _drawnTextureEntity = Scene.EntityManager.Find("drawnTexture");
            
            _centeredToast = new Entity("centeredToast")
                .AddComponent(new Sprite("Assets/Textures/toast_2D.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = 200,
                    DrawOrder = 0.1f,
                    XScale = 0.2f,
                    YScale = 0.2f,
                    Origin = Vector2.Center,
                });
            _centeredToast.IsVisible = false;

            bool[,] scaledGivenTexture = BoolToTextureConverter.ScaleTexture(_givenTexture, 100, 100);
            bool[,] scaledDrawnTexture = BoolToTextureConverter.ScaleTexture(_drawnTexture, 100, 100);
            _centeredTexture = new Entity("centeredTexture")
                .AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(scaledGivenTexture, scaledDrawnTexture, Scene.RenderManager)))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Opacity = 1,
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = 200,
                    DrawOrder = 0.05f,
                    Origin = Vector2.Center,
                });
            _centeredTexture.IsVisible = false;

            Scene.EntityManager.Add(_centeredToast);
            Scene.EntityManager.Add(_centeredTexture);
        }

        protected override void Update(TimeSpan gameTime)
        {
            UpdateTexturedToast(_givenTexture, _givenToastEntity, _givenTextureEntity);
            UpdateTexturedToast(_drawnTexture, _drawnToastEntity, _drawnTextureEntity);
        }

        private void UpdateTexturedToast(bool[,] texture, Entity toastEntity, Entity textureEntity)
        {
            if (toastEntity.FindComponent<Transform2D>().X != WaveServices.Platform.ScreenWidth / 2)
            {
                Move(toastEntity, textureEntity);
            }
            else
            {
                toastEntity.IsVisible = false;
                textureEntity.IsVisible = false;
                _centeredToast.IsVisible = true;
                _centeredTexture.IsVisible = true;
            }
        }

        private void Move(Entity toastEntity, Entity textureEntity)
        {
            if (toastEntity.FindComponent<Transform2D>().X < WaveServices.Platform.ScreenWidth / 2)
            {
                toastEntity.FindComponent<Transform2D>().X++;
                textureEntity.FindComponent<Transform2D>().X++;
            }
            else
            {
                toastEntity.FindComponent<Transform2D>().X--;
                textureEntity.FindComponent<Transform2D>().X--;
            }
        }
    }
}
