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

            //declare an array to hold the bytes of the bitmap
            byte[] pixels = new byte[pixelCount * 4];

            for (int i = 0; i < pixelCount; i++)
            {
                int width = drawMatrix.GetLength(0);
                int height = drawMatrix.GetLength(1);

                if (drawMatrix[i / drawMatrix.GetLength(0), i % drawMatrix.GetLength(1)])
                {
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

        public static bool[,] ScaleTexture(bool[,] texture, int width, int height)
        {
            bool[,] newTexture = new bool[width, height];

            for (int textureX = 0; textureX < texture.GetLength(0); textureX++)
            {
                for (int textureY = 0; textureY < texture.GetLength(1); textureY++)
                {
                    int startX = width / texture.GetLength(0) * textureX;
                    int startY = height / texture.GetLength(1) * textureY;
                    for (int newTextureX = startX; newTextureX < startX + width / texture.GetLength(0); newTextureX++)
                    {
                        for (int newTextureY = startY; newTextureY < startY + height / texture.GetLength(1); newTextureY++)
                        {
                            newTexture[newTextureX, newTextureY] = texture[textureX, textureY];
                        }
                    }
                }
            }

            return newTexture;
        }
    }
}
