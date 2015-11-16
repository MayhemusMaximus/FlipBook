using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FlipBook
{
    public class PaintScreen : BaseScreen
    {

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
                case DrawMode.Fill:
                    HandleFill();
                    break;
            }

            
            HandlePan();

            FrameManager.ActiveFrame.Grid.ShowGridLines = Globals.ShowGrid;
        }

        private void HandleFill()
        {
            if(Input.CurrentMouseState.LeftButton == ButtonState.Pressed && Input.PreviousMouseState.LeftButton == ButtonState.Released)
            {
                FillGrid(getGridCellMouseIsOver());
            }
        }

        private struct GridCheckStruct
        {
            public Vector2 ID { get; set; }
            
            public Boolean Change { get; set; }
            public Boolean LeftChecked { get; set; }
            public Boolean RightChecked { get; set; }
            public Boolean UpChecked { get; set; }
            public Boolean DownChecked { get; set; }
        }

        private void FillGrid(GridCell gridCell)
        {
            Color changeColor = gridCell.Color;

            //GridCheckStruct[,] checkStruct = new GridCheckStruct[(int)this.grid.GridSize.X, (int)this.grid.GridSize.Y];
            GridCheckStruct[,] checkStruct = new GridCheckStruct[(int)FrameManager.ActiveFrame.Grid.GridSize.X, (int)FrameManager.ActiveFrame.Grid.GridSize.Y];

            for(int x = 0; x < (int)this.grid.GridSize.X; x++)
            {
                for(int y = 0; y < (int)this.grid.GridSize.Y; y++)
                {
                    checkStruct[x, y].Change = false;
                    checkStruct[x, y].LeftChecked = false;
                    checkStruct[x, y].RightChecked = false;
                    checkStruct[x, y].UpChecked = false;
                    checkStruct[x, y].DownChecked = false;
                    checkStruct[x,y].ID = new Vector2(x,y);
                }
            }

            checkStruct[(int)gridCell.ID.X, (int)gridCell.ID.Y].Change = true;

            Boolean allChecked = false;
            while(!allChecked)
            {
                allChecked = true;

                GridCheckStruct[,] holdStruct = checkStruct;

                foreach(GridCheckStruct index in checkStruct)
                {
                    if(index.Change == true)
                    {
                        if((int)index.ID.X > 0)
                            if(!index.LeftChecked)
                            {
                                allChecked = false;
                                if( FrameManager.ActiveFrame.Grid.Cells[(int)index.ID.X - 1, (int)index.ID.Y].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X - 1, (int)index.ID.Y].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].LeftChecked = true;
                            }
                        if(index.ID.X < FrameManager.ActiveFrame.Grid.GridSize.X - 1)
                            if (!index.RightChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Cells[(int)index.ID.X + 1, (int)index.ID.Y].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X + 1, (int)index.ID.Y].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].RightChecked = true;
                            }
                        if(index.ID.Y > 0)
                            if (!index.UpChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Cells[(int)index.ID.X, (int)index.ID.Y - 1].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X, (int)index.ID.Y - 1].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].UpChecked = true;
                            }
                        if (index.ID.Y < FrameManager.ActiveFrame.Grid.GridSize.Y - 1)
                            if (!index.DownChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Cells[(int)index.ID.X, (int)index.ID.Y + 1].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X, (int)index.ID.Y + 1].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].DownChecked = true;
                            }
                    }
                }

                checkStruct = holdStruct;
            }

            foreach(GridCheckStruct check in checkStruct)
            {
                if(check.Change)
                {
                    FrameManager.ActiveFrame.Grid.Cells[(int)check.ID.X, (int)check.ID.Y].Color = Globals.DrawingColor;
                    //this.grid.Cells[(int)check.ID.X, (int)check.ID.Y].Color = Globals.DrawingColor;
                }
            }

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
                        return FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Center.ToVector2();
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

        private GridCell getGridCellMouseIsOver()
        {
            GridCell gc = new GridCell(new Vector2(0, 0), Color.White, new Vector2(0,0));

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.GridSize.Y; y++)
                {
                    if (FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds.Contains(Input.PreviousMousePosition))
                        return FrameManager.ActiveFrame.Grid.Cells[x, y];
                }
            }

            return gc;
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
