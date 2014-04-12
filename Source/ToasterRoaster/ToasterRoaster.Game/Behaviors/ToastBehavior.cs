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
            _toastRectangle = new RectangleF(Transform2D.X, Transform2D.Y, Transform2D.Rectangle.Width * Transform2D.XScale, Transform2D.Rectangle.Height * Transform2D.YScale);
            //_drawMatrix = new bool[(int)_toastRectangle.Width, (int)_toastRectangle.Height];
            _drawMatrix = new bool[5, 5];
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
                    _drawMatrix[(int)((firstTouch.Position.X - _toastRectangle.X) * (_drawMatrix.GetLength(0) / _toastRectangle.Width)),
                        (int)((firstTouch.Position.Y - _toastRectangle.Y) * (_drawMatrix.GetLength(1) / _toastRectangle.Height))] = true;
                    
                    int count = 0;
                    for (int i = 0; i < _drawMatrix.Length; i++)
                    {
                        if (_drawMatrix[i / _drawMatrix.GetLength(1), i % _drawMatrix.GetLength(1)])
                        {
                            count++;
                        }
                    }

                    Entity toast = EntityManager.Find<Entity>("toast");
                    toast.RemoveComponent<Sprite>();
                    toast.AddComponent(new Sprite(BoolToTextureConverter.TxdFromBoolArray(_drawMatrix, this.RenderManager)));

                    //Sprite = new Sprite(BoolToTextureConverter.TxdFromBoolArray(_drawMatrix, this.RenderManager));
                    
                    Transform2D.XScale = 20;
                    Transform2D.YScale = 20;

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
