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
        public DrawMode DrawMode { get; set; }
        public Boolean CanPan = true;
        public Boolean ShowGridLines = true;

        public Color DrawColor { get; set; }

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

        public Grid(Vector2 size, Vector2 position)
        {
            BuildTexture();
            Position = position;
            GridSize = size;
            makeBox();
            DrawColor = Color.Orange;
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

        private void RebuildGrid()
        {

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    Cells[x, y].Bounds = new Rectangle((int)Position.X + (x * Globals.Scale), (int)Position.Y + (y * Globals.Scale), Globals.Scale, Globals.Scale);
                }
            }
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
            if(Globals.ScaleChanged)
            {
                CellBorder = new Texture2D(Globals.GraphicsDevice, Globals.Scale, Globals.Scale);
                makeBox();
                RebuildGrid();
            }

            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                ColorCell(Input.CurrentMousePosition);

            HandlePan();
        }

        private void HandlePan()
        {
            if (Input.CurrentMouseState.MiddleButton == ButtonState.Pressed && Input.PreviousMouseState.MiddleButton == ButtonState.Pressed)
            {
                Vector2 currentPosition = Input.CurrentMousePosition.ToVector2();
                //Input.CurrentMousePosition.ToVector2();
                Vector2 previousPosition = Input.PreviousMousePosition.ToVector2();
                Vector2 delta = currentPosition - previousPosition;
                Pan(delta);
            }
        }

        private void Pan(Vector2 delta)
        {
            Position = Position + delta;
            RebuildGrid();
        }

        private void ColorCell(Point point)
        {
            foreach(GridCell cell in Cells)
            {
                if(cell.Bounds.Contains(point))
                {
                    if(this.DrawMode == DrawMode.Pencil)
                        cell.Color = DrawColor;
                    if (this.DrawMode == DrawMode.Eraser)
                        cell.Color = Color.White;
                    break;   
                }
            }
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
