using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class WorkSpaceScreen : BaseScreen
    {
        Texture2D ActiveFrame;

        Textbox tb = new Textbox("1", new Vector2(100, 100), new Vector2(75, 25));

        Grid grid = new Grid(new Vector2(16,16), new Vector2(0,0));

        Texture2D line = new Texture2D(Globals.GraphicsDevice, 1, 200);
        public void makeLine()
        {

            Color[] colors = new Color[line.Width * line.Height];
            line.GetData(colors);
            
            for(int x = 0; x < colors.Count(); x++)
            {
                colors[x] = Color.Red;
            }

            line.SetData(colors);
        }

        public WorkSpaceScreen(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;

            makeLine();
        }

        public override void Load()
        {
            
        }

        public override void Unload()
        {

        }

        public override void Update()
        {
            grid.Update();
        }

        Swatch swatch = new Swatch("Swatch", new Vector2(200,0), new Vector2(15,200));

        public override void Draw()
        {
            swatch.Draw();
            grid.Draw();
        }

        //private void DrawSwatch()
        //{
        //    Vector2 pos = new Vector2(200, 0);
        //    int swatchWidth = 15;
        //    int increment = 8;
        //    for(int g = 0; g < 256; g = g + increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(255, g, 0));
        //        pos.Y = pos.Y + 1;
        //    }
        //    for (int r = 255; r > -1; r = r - increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(r, 255, 0));
        //        pos.Y = pos.Y + 1;
        //    }
        //    for (int b = 0; b < 256; b = b + increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(0, 255, b));
        //        pos.Y = pos.Y + 1;
        //    }
        //    for (int g = 255; g > -1; g = g - increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(0, g, 255));
        //        pos.Y = pos.Y + 1;
        //    }
        //    for (int r = 0; r < 256; r = r + increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(r, 0, 255));
        //        pos.Y = pos.Y + 1;
        //    }
        //    for (int b = 255; b > -1; b = b - increment)
        //    {
        //        Globals.SpriteBatch.Draw(Textures.texture, new Rectangle((int)pos.X, (int)pos.Y, swatchWidth, 1), new Color(255, 0, b));
        //        pos.Y = pos.Y + 1;
        //    }
        //}
    }
}
