using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Math;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;

namespace ToasterRoaster.Game.Common
{
    public static class BoolToTextureConverter
    {
        private static byte[] Array1DFromBitmap(bool[,] drawMatrix)
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

        public static Texture2D TxdFromBoolArray(bool[,] drawMatrix, RenderManager RenderManager)
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
            RenderManager.GraphicsDevice.Textures.UploadTexture(tex2D);

            return tex2D;
        }
    }
}
