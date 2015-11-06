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

        int scale = 10;

        //private Vector2 gridSize = new Vector2(16, 16);
        //private Vector2 GridSize
        //{
        //    get { return this.gridSize; }
        //    set
        //    {
        //        this.gridSize = value;
        //        RebuildGrid();
        //    }
        //}

        Grid grid = new Grid(new Vector2(16,16), new Vector2(200,200));

        //Rectangle[,] grid;

        //private void RebuildGrid()
        //{
        //    grid = new Rectangle[(int)GridSize.X, (int)GridSize.Y];

        //    for(int x = 0; x < GridSize.X; x++)
        //    {
        //        for(int y = 0; y < GridSize.Y; y++)
        //        {
        //            grid[x, y] = new Rectangle((int)Position.X + (x * scale), (int)Position.Y + (y * scale), scale, scale);
        //        }
        //    }
        //}

        //Texture2D box = new Texture2D(Globals.GraphicsDevice, 10, 10);
        //public void makeBox()
        //{
        //    Color[] colors = new Color[box.Width * box.Height];
        //    box.GetData(colors);

        //    // left side
        //    for (int y = 0; y < box.Height; y++)
        //    {
        //        colors[y] = Color.Red;
        //    }

        //    // right side
        //    for (int y = colors.Count() - 1; y > colors.Count() - box.Height; y--)
        //    {
        //        colors[y] = Color.Red;
        //    }

        //    // Top
        //    for (int y = 0; y < colors.Count(); y = y + box.Height)
        //    {
        //        colors[y] = Color.Red;
        //    }

        //    // Bottom
        //    for (int y = box.Height - 1; y < colors.Count(); y = y + box.Height )
        //    {
        //        colors[y] = Color.Red;
        //    }

        //        box.SetData(colors);
        //}

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
            //makeBox();

            //GridSize = new Vector2(16, 16);
            
            //RebuildGrid();
        }

        public override void Load()
        {
            //base.Load();
        }

        public override void Unload()
        {
            //base.Unload();
        }

        public override void Update()
        {
            //base.Update();
            grid.Update();
        }

        public override void Draw()
        {
            //base.Draw();

            //Globals.SpriteBatch.Draw(Textures.TextBox, tb.Bounds, Color.White);
            //Globals.SpriteBatch.Draw(line, new Rectangle(10, 10, 10, 200), Color.White);

            //Globals.SpriteBatch.Draw(box, new Rectangle(200, 100, 200, 200), Color.Red);

            //for(int x = 0; x < GridSize.X; x++)
            //{
            //    for(int y = 0; y < GridSize.Y; y++)
            //    {
            //        Globals.SpriteBatch.Draw(box, grid[x, y], Color.White);
            //    }
            //}

            grid.Draw();
        }
    }
}
