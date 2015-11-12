using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Grid : BaseScreen
    {
        //public DrawMode DrawMode { get; set; }
        //public Boolean CanPan = true;
        //public Boolean CanZoom = true;
        public Boolean ShowGridLines = true;

        //public Color DrawColor { get; set; }

        private Vector2 gridSize;
        public Vector2 GridSize
        {
            get { return gridSize; }
            set 
            {
                gridSize = value;
                BuildGrid();
            }
        }
        public GridCell[,] Cells;

        //private Texture2D texture = new Texture2D(Globals.GraphicsDevice, 1, 1);

        private void BuildTexture()
        {
            Color[] color = new Color[Textures.texture.Width * Textures.texture.Height];
            Textures.texture.GetData(color);
            color[0] = Color.White;
            Textures.texture.SetData(color);
        }


        public Texture2D CellBorder = new Texture2D(Globals.GraphicsDevice, 10, 10);

        //public Grid(Vector2 position, Vector2 size, Vector2 pixels)
        public Grid(Vector2 position, Vector2 size)
        {
            BuildTexture();
            Position = position;
            this.Size = new Vector2(size.X * Globals.Scale, size.Y * Globals.Scale);
            GridSize = size;
            //GridSize = pixels;
            makeBox();
            //DrawColor = Color.Orange;
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

            Globals.GraphicsDevice.Textures[0] = Textures.texture;
        }

        public void RebuildGrid()
        {

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    Cells[x, y].Bounds = new Rectangle((int)Position.X + (x * Globals.Scale), (int)Position.Y + (y * Globals.Scale), Globals.Scale, Globals.Scale);
                }
            }
            //this.Size = new Vector2(GridSize.X * Globals.Scale, GridSize.Y * Globals.Scale);
        }


        private void BuildGrid()
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
        }

        public override void Draw()
        {

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    Globals.SpriteBatch.Draw(Textures.texture, Cells[x, y].Bounds, Cells[x, y].Color);
                    if(ShowGridLines)
                        Globals.SpriteBatch.Draw(CellBorder, Cells[x, y].Bounds, Color.White);
                }
            }
        }
    }
}