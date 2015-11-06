using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static Vector2 ThumbnailSize { get; set; }
        public static Vector2 ImageSize { get; set; }

        // Input
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState PreviousKeyboardState;

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

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
    }
}
