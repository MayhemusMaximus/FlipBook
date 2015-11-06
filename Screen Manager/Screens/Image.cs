using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Image
    {
        public Color[,] Pixels { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Image(int width, int height)
        {
            Pixels = new Color[width, height];
            Width = width;
            Height = height;
        }

        public void Initialize(Func<Color> createColor)
        {
            for(int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Pixels[x, y] = createColor();
                }
            }
        }

        public void Modify(Func<int, int, Color, Color> modifyColor)
        {
            for(int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color current = Pixels[x, y];
                    Pixels[x, y] = modifyColor(x, y, current);
                }
            }
        }
    }
}
