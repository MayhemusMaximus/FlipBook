using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FlipBook
{
    public static class PointExt
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point ToPoint(this Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }

        public static Texture2D ToTexture2D(this Grid grid)
        {
            Texture2D retVal = new Texture2D(Globals.GraphicsDevice, (int)Globals.ImageSize.X, (int)Globals.ImageSize.Y);
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[(int)Globals.ImageSize.X * (int)Globals.ImageSize.Y];
            int ndx = 0;
            for(int y = 0; y < Globals.ImageSize.Y; y++)
            {
                for(int x = 0; x < Globals.ImageSize.X; x++)
                {
                    colors[ndx] = grid.Cells[x, y].Color;
                    ndx++;
                }
            }

            retVal.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;
            return retVal;
        }

        public static Button ToButton(this string name, List<Button> Buttons)
        {
            Button retVal = new Button();

            foreach (Button button in Buttons)
            {
                if (button.Name == name)
                    return button;
            }

            return retVal;
        }
    }
}
