using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _drawMatrix = new bool[(int)_toastRectangle.Width, (int)_toastRectangle.Height];
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
                    for (int x = -5; x <= 5; x++)
                    {
                        for (int y = -5; y <= 5; y++)
                        {
                            _drawMatrix[(int)firstTouch.Position.X - (int)_toastRectangle.X + x, (int)firstTouch.Position.Y - (int)_toastRectangle.Y + y] = true; 
                        }
                        
                    }//int count = 0;
                    //for (int i = 0; i < _drawMatrix.Length; i++)
                    //{
                    //    if (_drawMatrix[i / 280, i % 280])
                    //    {
                    //        count++;
                    //    }
                    //}

                    Entity toast = EntityManager.Find<Entity>("toast");
                    toast.RemoveComponent<Sprite>();
                    toast.AddComponent(new Sprite(TxdFromBoolArray(_drawMatrix)));
                }
            }
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
        }

        public static byte[] Array1DFromBitmap(bool[,] drawMatrix)
        {
            // get total locked pixels count
            int pixelCount = drawMatrix.Length;

            // Create rectangle to lock
            Rectangle rect = new Rectangle(0, 0, drawMatrix.GetLength(0), drawMatrix.GetLength(1));

            //declare an array to hold the bytes of the bitmap
            byte[] pixels = new byte[pixelCount * 4];

            for (int i = 0; i < pixelCount; i++)
            {
                int width = drawMatrix.GetLength(0);
                int height = drawMatrix.GetLength(1);

                if (drawMatrix[i / drawMatrix.GetLength(0), i % drawMatrix.GetLength(1)])
                {
                    pixels[4 * i] = Byte.MaxValue;
                    pixels[4 * i + 1] = Byte.MaxValue;
                    pixels[4 * i + 2] = Byte.MaxValue;
                    pixels[4 * i + 3] = Byte.MaxValue;
                }
            }

            return pixels;
        }
        Texture2D TxdFromBoolArray(bool[,] drawMatrix)
        //byte[][][] TxdFromBoolArray(bool[,] drawMatrix)
        {
            Texture2D tex2D = new Texture2D()
            {
                Format = WaveEngine.Common.Graphics.PixelFormat.R8G8B8A8,
                Height = drawMatrix.GetLength(1),
                Width = drawMatrix.GetLength(0),
                Levels = 1
            };

            tex2D.Data = new byte[1][][];
            tex2D.Data[0] = new byte[1][];
            tex2D.Data[0][0] = Array1DFromBitmap(drawMatrix);
            //RenderManager.GraphicsDevice.Textures.UploadTexture(tex2D);

            return tex2D;
        }
    }
}
