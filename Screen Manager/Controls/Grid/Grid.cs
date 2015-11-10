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

        public Grid(Vector2 position, Vector2 size, Vector2 pixels)
        {
            BuildTexture();
            Position = position;
            this.Size = size * Globals.Scale;
            GridSize = pixels;
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
            this.Size = new Vector2(GridSize.X * Globals.Scale, GridSize.Y * Globals.Scale);
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

            switch(this.DrawMode)
            {
                case DrawMode.Pencil:
                    HandlePencil();
                    break;
                case DrawMode.Eraser:
                    HandleEraser();
                    break;
                case DrawMode.Line:
                    HandleLine();
                    break;
            }


            HandlePan();
        }

        private void HandlePencil()
        {
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                ColorCell(Input.CurrentMousePosition, this.DrawColor);
        }

        private void HandleEraser()
        {
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                ColorCell(Input.CurrentMousePosition, Color.White);
        }

        private Boolean drawingLine = false;
        private Vector2 LineStart = Vector2.Zero;
        private void HandleLine()
        {
            if(!drawingLine)
            {
                if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed && Input.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    if (this.Bounds.Contains(Input.CurrentMousePosition))
                    {
                        for (int x = 0; x < this.GridSize.X; x++)
                        {
                            for (int y = 0; y < this.GridSize.Y; y++)
                            {
                                if (Cells[x, y].Bounds.Contains(Input.CurrentMousePosition))
                                {
                                    drawingLine = true;
                                    //LineStart = Input.CurrentMousePosition.ToVector2();
                                    LineStart = new Vector2((Cells[x, y].Bounds.Right + Cells[x, y].Bounds.X) - Cells[x, y].Bounds.X, (Cells[x, y].Bounds.Bottom + Cells[x, y].Bounds.Y) - Cells[x, y].Bounds.Y);
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                if (Input.PreviousMouseState.LeftButton == ButtonState.Released && Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (this.Bounds.Contains(Input.CurrentMousePosition))
                    {
                        drawingLine = false;
                        createLine();
                    }
                }
            }
        }

        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
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

        private void ColorCell(Point point, Color color)
        {
            foreach(GridCell cell in Cells)
            {
                if(cell.Bounds.Contains(point))
                {
                    cell.Color = color;
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

            if (drawingLine)
                DrawLine();
        }

        public void DrawLine()
        {
            if(this.Bounds.Contains(Input.CurrentMousePosition))
            {
                for (int x = 0; x < this.GridSize.X; x++)
                {
                    for (int y = 0; y < this.GridSize.Y; y++)
                    {
                        if (Cells[x, y].Bounds.Contains(Input.CurrentMousePosition))
                        {
                            lineEnd = new Vector2((Cells[x, y].Bounds.Right + Cells[x,y].Bounds.X) - Cells[x, y].Bounds.X, (Cells[x, y].Bounds.Bottom + Cells[x, y].Bounds.Y) - Cells[x, y].Bounds.Y);
                        }
                    }
                }
            }
            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    if(LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), Cells[x,y].Bounds))
                    {
                        Globals.SpriteBatch.Draw(Textures.texture, Cells[x, y].Bounds, DrawColor);
                    }
                }
            }
        }

        Vector2 lineEnd = Vector2.Zero;
        private void createLine()
        {
            for (int x = 0; x < this.GridSize.X; x++)
            {
                for (int y = 0; y < this.GridSize.Y; y++)
                {
                    if (Cells[x, y].Bounds.Contains(Input.CurrentMousePosition))
                    {
                        lineEnd = new Vector2((Cells[x, y].Bounds.Right + Cells[x, y].Bounds.X) - Cells[x, y].Bounds.X, (Cells[x, y].Bounds.Bottom + Cells[x, y].Bounds.Y) - Cells[x, y].Bounds.Y);
                    }
                }
            }

            for (int x = 0; x < GridSize.X; x++)
            {
                for (int y = 0; y < GridSize.Y; y++)
                {
                    //Globals.SpriteBatch.Draw(Textures.texture, Cells[x, y].Bounds, Cells[x, y].Color);
                    if (LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), Cells[x, y].Bounds))
                    {
                        Cells[x, y].Color = this.DrawColor;
                    }
                }
            }
        }
    }
}