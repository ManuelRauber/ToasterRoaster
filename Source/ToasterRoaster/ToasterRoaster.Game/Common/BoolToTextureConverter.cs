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
        private static byte[] Array1DFromBool(bool[,] drawMatrix)
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
                    pixels[4 * i + 0] = 79;
                    pixels[4 * i + 1] = 39;
                    pixels[4 * i + 2] = 0;
                    pixels[4 * i + 3] = Byte.MaxValue;
                }
            }

            return pixels;
        }

        private static byte[] Array1DFromBool(bool[,] givenTexture, bool[,] drawnTexture)
        {
            if (!(givenTexture.GetLength(0) == drawnTexture.GetLength(0) && givenTexture.GetLength(1) == drawnTexture.GetLength(1)))
            {
                throw new InvalidOperationException("The bounds of the arrays do not match.");
            }
            // get total locked pixels count
            int pixelCount = givenTexture.Length;

            //declare an array to hold the bytes of the bitmap
            byte[] pixels = new byte[pixelCount * 4];

            for (int i = 0; i < pixelCount; i++)
            {
                int width = givenTexture.GetLength(0);
                int height = givenTexture.GetLength(1);

                if (givenTexture[i / givenTexture.GetLength(0), i % givenTexture.GetLength(1)]
                    && drawnTexture[i / drawnTexture.GetLength(0), i % drawnTexture.GetLength(1)])
                {
                    pixels[4 * i + 0] = 79;
                    pixels[4 * i + 1] = 39;
                    pixels[4 * i + 2] = 0;
                    pixels[4 * i + 3] = Byte.MaxValue;
                }
                else if (givenTexture[i / givenTexture.GetLength(0), i % givenTexture.GetLength(1)]
                    && !drawnTexture[i / drawnTexture.GetLength(0), i % drawnTexture.GetLength(1)])
                {
                    pixels[4 * i + 0] = 255;
                    pixels[4 * i + 1] = 0;
                    pixels[4 * i + 2] = 0;
                    pixels[4 * i + 3] = Byte.MaxValue;
                }
                else if (!givenTexture[i / givenTexture.GetLength(0), i % givenTexture.GetLength(1)]
                    && drawnTexture[i / drawnTexture.GetLength(0), i % drawnTexture.GetLength(1)])
                {
                    pixels[4 * i + 0] = 0;
                    pixels[4 * i + 1] = 0;
                    pixels[4 * i + 2] = 255;
                    pixels[4 * i + 3] = Byte.MaxValue;
                }
            }

            return pixels;
        }

        public static Texture2D TxdFromBoolArray(bool[,] drawMatrix, RenderManager RenderManager)
        //byte[][][] TxdFromBoolArray(bool[,] drawMatrix)
        {
            Texture2D tex2D = CreateTexture(drawMatrix);
            tex2D.Data[0][0] = Array1DFromBool(drawMatrix);
            RenderManager.GraphicsDevice.Textures.UploadTexture(tex2D);

            return tex2D;
        }

        public static Texture2D TxdFromBoolArray(bool[,] givenTexture, bool[,] drawnTexture, RenderManager RenderManager)
        //byte[][][] TxdFromBoolArray(bool[,] drawMatrix)
        {
            Texture2D tex2D = CreateTexture(drawnTexture);
            tex2D.Data[0][0] = Array1DFromBool(givenTexture, drawnTexture);
            RenderManager.GraphicsDevice.Textures.UploadTexture(tex2D);

            return tex2D;
        }

        private static Texture2D CreateTexture(bool[,] drawMatrix)
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
