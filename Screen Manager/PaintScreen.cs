using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class PaintScreen : BaseScreen
    {

        //private Grid grid = new Grid(new Vector2(0, 30), new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, Globals.Graphics.PreferredBackBufferHeight - 200 - 30), new Vector2(32, 32));
        private Grid grid = new Grid(new Vector2(0, 30), Globals.ImageSize);

        public PaintScreen(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;

            grid.ShowGridLines = Globals.ShowGrid;
        }

        public override void Update()
        {
            if (Globals.ScaleChanged)
                HandleZoom();

            switch (Globals.DrawMode)
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

            grid.ShowGridLines = Globals.ShowGrid;
        }

        public override void Draw()
        {
            grid.Draw();

            if (drawingLine)
                DrawLine();
        }

        private void HandleZoom()
        {
            grid.CellBorder = new Texture2D(Globals.GraphicsDevice, Globals.Scale, Globals.Scale);
            grid.makeBox();
            grid.RebuildGrid();
        }

        private void HandlePencil()
        {
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                ColorCell(Input.CurrentMousePosition, Globals.DrawingColor);
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
            if (!drawingLine)
            {
                if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed && Input.PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    if (this.Bounds.Contains(Input.CurrentMousePosition))
                    {
                        drawingLine = true;
                        LineStart = MouseDrawPoint();
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

        private void HandlePan()
        {
            if (Input.CurrentMouseState.MiddleButton == ButtonState.Pressed && Input.PreviousMouseState.MiddleButton == ButtonState.Pressed)
            {
                Vector2 currentPosition = Input.CurrentMousePosition.ToVector2();
                Vector2 previousPosition = Input.PreviousMousePosition.ToVector2();
                Vector2 delta = currentPosition - previousPosition;
                Pan(delta);
            }
        }

        private void Pan(Vector2 delta)
        {
            this.grid.Position = this.grid.Position + delta;
            this.grid.RebuildGrid();
        }

        public Vector2 MouseDrawPoint()
        {
            Vector2 retVal = Vector2.Zero;

            for (int x = 0; x < this.grid.GridSize.X; x++)
            {
                for (int y = 0; y < this.grid.GridSize.Y; y++)
                {
                    if (this.grid.Cells[x, y].Bounds.Contains(Input.CurrentMousePosition))
                    {
                        return new Vector2((this.grid.Cells[x, y].Bounds.Right + this.grid.Cells[x, y].Bounds.X) - this.grid.Cells[x, y].Bounds.X, (this.grid.Cells[x, y].Bounds.Bottom + this.grid.Cells[x, y].Bounds.Y) - this.grid.Cells[x, y].Bounds.Y);
                    }
                }
            }

            return retVal;
        }

        public void DrawLine()
        {
            if(this.grid.Bounds.Contains(Input.CurrentMousePosition))
                lineEnd = MouseDrawPoint();

            for (int x = 0; x < this.grid.GridSize.X; x++)
            {
                for (int y = 0; y < this.grid.GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), this.grid.Cells[x, y].Bounds))
                    {
                        Globals.SpriteBatch.Draw(Textures.texture, this.grid.Cells[x, y].Bounds, Globals.DrawingColor);
                    }
                }
            }
        }

        Vector2 lineEnd = Vector2.Zero;
        public void createLine()
        {
            lineEnd = MouseDrawPoint();

            for (int x = 0; x < this.grid.GridSize.X; x++)
            {
                for (int y = 0; y < this.grid.GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), this.grid.Cells[x, y].Bounds))
                    {
                        this.grid.Cells[x, y].Color = Globals.DrawingColor;
                    }
                }
            }
        }

        public void ColorCell(Point point, Color color)
        {
            foreach (GridCell cell in this.grid.Cells)
            {
                if (cell.Bounds.Contains(point))
                {
                    cell.Color = color;
                    break;
                }
            }
        }
    }
}
