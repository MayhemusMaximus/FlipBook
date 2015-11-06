using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Swatch : BaseScreen
    {
        public Swatch()
        {
            Name = "Swatch Control";
            Position = new Vector2(200, 200);
            Size = new Vector2(15, 200);
        }

        public Swatch(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;
        }

        public override void Draw()
        {

            //Vector2 Position = new Vector2(200, 0);
            int swatchWidth = 15;
            int increment = 8;
            int y = (int)Position.Y;


            for (int g = 0; g < 256; g = g + increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(255, g, 0));
                y++;
            }
            for (int r = 255; r > -1; r = r - increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(r, 255, 0));
                y++;
            }
            for (int b = 0; b < 256; b = b + increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(0, 255, b));
                y++;
            }
            for (int g = 255; g > -1; g = g - increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(0, g, 255));
                y++;
            }
            for (int r = 0; r < 256; r = r + increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(r, 0, 255));
                y++;
            }
            for (int b = 255; b > -1; b = b - increment)
            {
                Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)Position.X, y, (int)Size.X, 1), new Color(255, 0, b));
                y++;
            }
        }
    }
}
