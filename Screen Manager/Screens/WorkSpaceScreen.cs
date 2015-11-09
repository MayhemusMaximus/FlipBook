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
        Textbox tb = new Textbox("1", new Vector2(100, 100), new Vector2(75, 25));

        Grid grid = new Grid(new Vector2(32,32), new Vector2(0,0));

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

        public override void Update()
        {
            grid.DrawColor = swatch.PencilColor;
            grid.Update();
            swatch.Update();
        }

        Swatch swatch = new Swatch("Swatch", new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300,0), new Vector2(300,Globals.Graphics.PreferredBackBufferHeight - 200));

        public override void Draw()
        {
            grid.Draw();
            swatch.Draw();
        }
    }
}
