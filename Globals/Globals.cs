using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public static class Globals
    {
        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice GraphicsDevice;
        public static GraphicsDeviceManager Graphics;
        public static Vector2 GameSize;

        public static Vector2 ThumbnailSize { get; set; }
        public static Vector2 ImageSize { get; set; }

        public static Boolean MouseIsVisible { get; set; }

        public static Boolean ScaleChanged = false;
        private static int scale = 10;
        public static int Scale
        {
            get { return scale; }
            set
            {
                if (value > 0)
                    scale = value;
            }
        }

        private static Color drawingColor = Color.Orange;
        public static Color DrawingColor { get { return drawingColor; } set { drawingColor = value; } }

        private static DrawMode drawMode = DrawMode.Pencil;
        public static DrawMode DrawMode { get { return drawMode; } set { drawMode = value; } }

        private static Boolean showGrid = true;
        public static Boolean ShowGrid { get { return showGrid; } set { showGrid = value; } }
    }
}
