using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class GridCell
    {
        // Bounds will be used for collision detection
        public Rectangle Bounds;
        public Color Color;

        public GridCell(Vector2 position, Color color)
        {
            Bounds = new Rectangle((int)position.X, (int)position.Y, Globals.Scale, Globals.Scale);
            Color = color;
        }
    }
}
