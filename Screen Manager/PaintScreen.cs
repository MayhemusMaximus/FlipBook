using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            FrameManager.ActiveFrame.Grid.ShowGridLines = Globals.ShowGrid;
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

            FrameManager.ActiveFrame.Grid.ShowGridLines = Globals.ShowGrid;
        }

        public override void Draw()
        {
            FrameManager.ActiveFrame.Grid.Draw();

            if (drawingLine)
                DrawLine();
        }

        private void HandleZoom()
        {
            FrameManager.ActiveFrame.Grid.CellBorder = new Texture2D(Globals.GraphicsDevice, Globals.Scale, Globals.Scale);
            FrameManager.ActiveFrame.Grid.makeBox();
            FrameManager.ActiveFrame.Grid.RebuildGrid();
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
            FrameManager.ActiveFrame.Grid.Position = FrameManager.ActiveFrame.Grid.Position + delta;
            FrameManager.ActiveFrame.Grid.RebuildGrid();
        }

        public Vector2 MouseDrawPoint()
        {
            Vector2 retVal = Vector2.Zero;

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.GridSize.Y; y++)
                {
                    if (FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Contains(Input.CurrentMousePosition))
                    {
                        return new Vector2((FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Right + FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.X) - FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.X, (FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Bottom + FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Y) - FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Y);
                    }
                }
            }

            return retVal;
        }

        public void DrawLine()
        {
            if(FrameManager.ActiveFrame.Grid.Bounds.Contains(Input.CurrentMousePosition))
                lineEnd = MouseDrawPoint();

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds))
                    {
                        Globals.SpriteBatch.Draw(Textures.texture, FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds, Globals.DrawingColor);
                    }
                }
            }
        }

        Vector2 lineEnd = Vector2.Zero;
        public void createLine()
        {
            lineEnd = MouseDrawPoint();

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(LineStart.ToPoint(), lineEnd.ToPoint(), FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds))
                    {
                        FrameManager.ActiveFrame.Grid.Cells[x, y].Color = Globals.DrawingColor;
                    }
                }
            }
        }

        public void ColorCell(Point point, Color color)
        {
            foreach (GridCell cell in FrameManager.ActiveFrame.Grid.Cells)
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
