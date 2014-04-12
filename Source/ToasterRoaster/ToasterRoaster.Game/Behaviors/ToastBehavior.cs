using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Common;
using ToasterRoaster.Game.Services;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Behaviors
{
    class ToastBehavior : Behavior
    {
        private RectangleF _toastRectangle;
        private bool[,] _drawMatrix;


        [RequiredComponent]
        public Transform2D Transform2D;
        [RequiredComponent]
        public Sprite Sprite;

        public ToastBehavior()
            : base("ToastBehavior")
        {
            Transform2D = null;
            Sprite = null;
        }

        protected override void Initialize()
        {
            float width = Transform2D.Rectangle.Width * Transform2D.XScale;
            float height = Transform2D.Rectangle.Height * Transform2D.YScale;
            _toastRectangle = new RectangleF(Transform2D.X - (width * Transform2D.Origin.X),
                Transform2D.Y - (height * Transform2D.Origin.Y),
                width,
                height);

            _drawMatrix = new bool[5, 5];
            WaveServices.GetService<TextureMapService>().DrawnTexture = _drawMatrix;
            EntityManager.Add(new Entity("drawnTexture"));
            base.Initialize();
        }

        protected override void Update(TimeSpan gameTime)
        {
            var touches = WaveServices.Input.TouchPanelState;
            if (touches.Count > 0)
            {
                var firstTouch = touches[0];
                if (_toastRectangle.Contains(firstTouch.Position))
                {
                    _drawMatrix[(int)((firstTouch.Position.Y - _toastRectangle.Y) * (_drawMatrix.GetLength(1) / _toastRectangle.Height)),
                        (int)((firstTouch.Position.X - _toastRectangle.X) * (_drawMatrix.GetLength(0) / _toastRectangle.Width))] = true;
                    
                    int count = 0;
                    for (int i = 0; i < _drawMatrix.Length; i++)
                    {
                        if (_drawMatrix[i / _drawMatrix.GetLength(1), i % _drawMatrix.GetLength(1)])
                        {
                            count++;
                        }
                    }

                    EntityManager.Remove("drawnTexture");

                    bool[,] scaledTexture = BoolToTextureConverter.ScaleTexture(_drawMatrix, 100, 100);

                    Entity drawnTexture = new Entity("drawnTexture")
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
                    EntityManager.Add(drawnTexture);

                    //Owner.RemoveComponent<Sprite>();
                    //Owner.AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(_drawMatrix, this.RenderManager)));

                    //Sprite = new Sprite(BoolToTextureConverter.TxdFromBoolArray(_drawMatrix, this.RenderManager));
                    
                    WaveServices.GetService<TextureMapService>().DrawnTexture = _drawMatrix;
                }
            }
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
        }
    }
}
