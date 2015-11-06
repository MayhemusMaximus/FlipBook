using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Grid : BaseScreen
    {
        public Boolean ShowGridLines = true;

        private Vector2 gridSize;
        public Vector2 GridSize
        {
            get { return gridSize; }
            set 
            {
                gridSize = value;
                RebuildGrid();
            }
        }
        public GridCell[,] Cells;

        private Texture2D texture = new Texture2D(Globals.GraphicsDevice, 1, 1);

        private void BuildTexture()
        {
            Color[] color = new Color[texture.Width * texture.Height];
            texture.GetData(color);
            color[0] = Color.White;
            texture.SetData(color);
        }

        //public int scale;


        public Texture2D CellBorder = new Texture2D(Globals.GraphicsDevice, 10, 10);

        public Grid(Vector2 size, Vector2 position)
        {
            BuildTexture();
            Position = position;
            //this.scale = scale;
            GridSize = new Vector2(16, 16);
            makeBox();
        }

        public void makeBox()
        {
            
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[CellBorder.Width * CellBorder.Height];
            CellBorder.GetData(colors);

            // left side
            for (int y = 0; y < CellBorder.Height; y++)
            {
                colors[y] = Color.Gray;
            }

            // right side
            for (int y = colors.Count() - 1; y > colors.Count() - CellBorder.Height; y--)
            {
                colors[y] = Color.Gray;
            }

            // Top
            for (int y = 0; y < colors.Count(); y = y + CellBorder.Height)
            {
                colors[y] = Color.Gray;
            }

            // Bottom
            for (int y = CellBorder.Height - 1; y < colors.Count(); y = y + CellBorder.Height)
            {
                colors[y] = Color.Gray;
            }

            CellBorder.SetData(colors);

            Globals.GraphicsDevice.Textures[0] = texture;
        }

        private void RebuildGrid()
        {
            Cells = new GridCell[(int)GridSize.X, (int)GridSize.Y];

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    Cells[x, y] = new GridCell(new Vector2((int)Position.X + (x * Globals.Scale), (int)Position.Y + (y * Globals.Scale)), Color.White);
                }
            }
        }

        
        public override void Update()
        {
            if(Globals.ScaleChanged)
            {
                CellBorder = new Texture2D(Globals.GraphicsDevice, Globals.Scale, Globals.Scale);
                makeBox();
                RebuildGrid();
            }
        }

        public override void Draw()
        {

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    Globals.SpriteBatch.Draw(texture, Cells[x, y].Bounds, Cells[x, y].Color);
                    if(ShowGridLines)
                        Globals.SpriteBatch.Draw(CellBorder, Cells[x, y].Bounds, Color.White);
                }
            }
        }
    }
}
