using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public static class Textures
    {
        public static Texture2D TextBox;
        public static Texture2D texture = new Texture2D(Globals.GraphicsDevice, 1, 1);
        public static Texture2D Pencil;
        public static Texture2D Eraser;
        public static Texture2D Line;
        public static Texture2D Grid;
        public static Texture2D AddFrame;
        public static Texture2D DeleteFrame;
        public static Texture2D PaintCan;
        public static Texture2D Rectangle;
        public static Texture2D Circle;

        public static Texture2D SimpleTexture;

        public static void BuildSimpleTexture()
        {
            SimpleTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);

            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[1];
            colors[0] = Color.White;
            SimpleTexture.SetData(colors);

            Globals.GraphicsDevice.Textures[0] = hold;
        }
    }
}
