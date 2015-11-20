using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlipBook
{
    public class PaintScreen : BaseScreen
    {
        #region Todo

        // TODO: Implement Select Feature for Copy, Move, Paste, etc..

        #endregion


        #region Variables


        private Grid grid = new Grid(new Vector2(0, 30), Globals.ImageSize);

        private Boolean drawingLine = false;
        private Boolean drawingRectangle = false;
        private Boolean drawingCircle = false;
        private Vector2 start = Vector2.Zero;
        private Vector2 end = Vector2.Zero;

        private struct GridCheckStruct
        {
            public Vector2 ID { get; set; }

            //private Boolean change { get; set; }
            private Boolean leftChecked { get; set; }
            private Boolean rightChecked { get; set; }
            private Boolean upChecked { get; set; }
            private Boolean downChecked { get; set; }

            public Boolean AllChecked { get; private set; }
            public Boolean Change { get; set; }
            public Boolean LeftChecked
            {
                get { return leftChecked; }
                set
                {
                    leftChecked = value;
                    AllChecked = (LeftChecked && RightChecked && UpChecked && DownChecked);
                }
            }
            public Boolean RightChecked
            {
                get { return rightChecked; }
                set
                {
                    rightChecked = value;
                    AllChecked = (LeftChecked && RightChecked && UpChecked && DownChecked);
                }
            }
            public Boolean UpChecked
            {
                get { return upChecked; }
                set
                {
                    upChecked = value;
                    AllChecked = (LeftChecked && RightChecked && UpChecked && DownChecked);
                }
            }
            public Boolean DownChecked
            {
                get { return downChecked; }
                set
                {
                    downChecked = value;
                    AllChecked = (LeftChecked && RightChecked && UpChecked && DownChecked);
                }
            }
        }

        #endregion Variables

        #region Constructor

        public PaintScreen(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;

            FrameManager.ActiveFrame.Grid.Peek().ShowGridLines = Globals.ShowGrid;
        }

        #endregion Constructor

        #region XNA Loop Methods

        public override void Update()
        {
            HandleUndo();

            UpdateScale();

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
                case DrawMode.Rectangle:
                    HandleRectangle();
                    break;
                case DrawMode.Circle:
                    HandleCircle();
                    break;
            }

            
            HandlePan();

            FrameManager.ActiveFrame.Grid.Peek().ShowGridLines = Globals.ShowGrid;
        }
        public override void Draw()
        {
            FrameManager.ActiveFrame.Grid.Peek().Draw();

            if (drawingLine)
                drawLine(start, end);

            if (drawingRectangle)
                DrawRectangle();

            if (drawingCircle)
                DrawCircle();
        }
        /// <summary>
        /// This will draw a non-static line, so it can be moved by mouse position.
        /// </summary>
        private void drawLine(Vector2 start, Vector2 end)
        {
            //if (FrameManager.ActiveFrame.Grid.Bounds.Contains(Input.CurrentMousePosition))
            //end = MouseDrawPoint(Input.CurrentMousePosition);

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.Peek().GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.Peek().GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(start.ToPoint(), end.ToPoint(), FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds))
                    {
                        Globals.SpriteBatch.Draw(Textures.texture, FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds, Globals.DrawingColor);
                    }
                }
            }
        }

        private void DrawRectangle()
        {
            end = MouseDrawPoint(Input.CurrentMousePosition);

            drawLine(start, new Vector2(start.X, end.Y));
            drawLine(new Vector2(start.X, end.Y), end);
            drawLine(end, new Vector2(end.X, start.Y));
            drawLine(new Vector2(end.X, start.Y), start);

        }

        private void DrawCircle()
        {
            Vector2 drawEnd = MouseDrawPoint(Input.CurrentMousePosition);
            Vector2 center = new Rectangle((int)start.X, (int)start.Y, (int)drawEnd.X - (int)start.X, (int)drawEnd.Y - (int)start.Y).Center.ToVector2();

            int a = Math.Abs((int)(start.X - drawEnd.X)/2);
            int b = Math.Abs((int)(start.Y - drawEnd.Y)/2);

            circlePoints.Clear();

            double theta = 0;
            int h = (int)center.X;
            int k = (int)center.Y;

            double step = .261799387799; // 15 degrees

            double twoPi = 6.2831853071; // 360 degrees

            while (theta < twoPi)
            {
                int x = (int)(h + (a * Math.Cos(theta)));
                int y = (int)(k + (b * Math.Sin(theta)));
                circlePoints.Add(new Vector2(x, y));

                theta += step;
            }

            for (int i = 0; i < circlePoints.Count - 1; i++)
            {
                drawLine(circlePoints[i], circlePoints[i + 1]);
            }
            drawLine(circlePoints[circlePoints.Count - 1], circlePoints[0]);
        }
        #endregion XNA Loop Methods

        #region Handlers

        private static void HandleUndo()
        {
            if (FrameManager.ActiveFrame.Grid.Count > 1)
            {
                if (Input.CurrentKeyboardState.IsKeyDown(Keys.F2) && !Input.PreviousKeyboardState.IsKeyDown(Keys.F2))
                    FrameManager.ActiveFrame.Grid.Pop();
            }
        }

        private void HandleZoom()
        {
            FrameManager.ActiveFrame.Grid.Peek().CellBorder = new Texture2D(Globals.GraphicsDevice, Globals.Scale, Globals.Scale);
            FrameManager.ActiveFrame.Grid.Peek().makeBox();
            FrameManager.ActiveFrame.Grid.Peek().RebuildGrid();
        }

        private void HandlePan()
        {
            // TODO: Implement MiddleButton evaluation in Input
            if (Input.CurrentMouseState.MiddleButton == ButtonState.Pressed && Input.PreviousMouseState.MiddleButton == ButtonState.Pressed)
            {
                Vector2 currentPosition = Input.CurrentMousePosition.ToVector2();
                Vector2 previousPosition = Input.PreviousMousePosition.ToVector2();
                Vector2 delta = currentPosition - previousPosition;
                Pan(delta);
            }
        }

        private void HandlePencil()
        {
            if (Input.MouseLeftButtonState == MouseButtonState.Pressed)
                ColorCell(Input.CurrentMousePosition, Globals.DrawingColor);
            else if (Input.MouseLeftButtonState == MouseButtonState.Held)
            {
                GridCell gc1 = getMouseOverGridCell(Input.PreviousMousePosition);
                GridCell gc2 = getMouseOverGridCell(Input.CurrentMousePosition);
                Vector2 gc1ID = gc1.ID;
                Vector2 gc2ID = gc2.ID;
                if ((gc2ID - gc1ID).Length() > 1)
                    createLine(MouseDrawPoint(Input.PreviousMousePosition), MouseDrawPoint(Input.CurrentMousePosition));
                else
                    ColorCell(Input.CurrentMousePosition, Globals.DrawingColor);
            }
        }

        private void HandleEraser()
        {
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                FrameManager.ActiveFrame.Grid.Push(FrameManager.ActiveFrame.Grid.Peek().Clone());
                ColorCell(Input.CurrentMousePosition, Color.White);
            }
        }

        private void HandleLine()
        {
            if (!drawingLine)
            {
                if (Input.MouseLeftButtonState == MouseButtonState.Pressed)
                {
                    //if (this.Bounds.Contains(Input.CurrentMousePosition))
                    //{
                        drawingLine = true;
                        start = MouseDrawPoint(Input.CurrentMousePosition);
                    //}
                }

            }
            else
            {
                end = MouseDrawPoint(Input.CurrentMousePosition);
                if (Input.MouseLeftButtonState == MouseButtonState.Released)
                {
                    //if (this.Bounds.Contains(Input.CurrentMousePosition))
                    //{
                    drawingLine = false;

                    FrameManager.ActiveFrame.Grid.Push(FrameManager.ActiveFrame.Grid.Peek().Clone());

                    createLine(start, MouseDrawPoint(Input.CurrentMousePosition));
                    //}
                }
            }
        }

        private void HandleRectangle()
        {
            if (!drawingRectangle)
            {
                if (Input.MouseLeftButtonState == MouseButtonState.Pressed)
                {
                    if (this.Bounds.Contains(Input.CurrentMousePosition))
                    {
                        drawingRectangle = true;
                        start = MouseDrawPoint(Input.CurrentMousePosition);
                    }
                }

            }
            else
            {
                if (Input.MouseLeftButtonState == MouseButtonState.Released)
                {
                    if (this.Bounds.Contains(Input.CurrentMousePosition))
                    {
                        drawingRectangle = false;

                        FrameManager.ActiveFrame.Grid.Push(FrameManager.ActiveFrame.Grid.Peek().Clone());
                        createRectangle(start, MouseDrawPoint(Input.CurrentMousePosition));
                    }
                }
            }
        }

        private void HandleCircle()
        {

            if (!drawingCircle)
            {
                if (Input.MouseLeftButtonState == MouseButtonState.Pressed)
                {
                    //if (this.Bounds.Contains(Input.CurrentMousePosition))
                    //{
                        drawingCircle = true;
                        start = MouseDrawPoint(Input.CurrentMousePosition);
                    //}
                }

            }
            else
            {
                if (Input.MouseLeftButtonState == MouseButtonState.Released)
                {
                    //if (this.Bounds.Contains(Input.CurrentMousePosition))
                    //{
                    drawingCircle = false;
                    FrameManager.ActiveFrame.Grid.Push(FrameManager.ActiveFrame.Grid.Peek().Clone());
                        createCircle(start, MouseDrawPoint(Input.CurrentMousePosition));
                    //}
                }
            }
        }
        private void HandleFill()
        {
            if (Input.MouseLeftButtonState == MouseButtonState.Pressed)
            {
                FrameManager.ActiveFrame.Grid.Push(FrameManager.ActiveFrame.Grid.Peek().Clone());
                FillGrid(getMouseOverGridCell(Input.CurrentMousePosition));
            }
        }

        #endregion Handlers

        #region Handler Helpers

        #region Zoom

        /// <summary>
        /// Used to set the scale of the grid using the mouse wheel.
        /// </summary>
        private void UpdateScale()
        {
            if (Input.CurrentMouseState.ScrollWheelValue != Input.PreviousMouseState.ScrollWheelValue)
            {
                Globals.ScaleChanged = true;
                if (Input.CurrentMouseState.ScrollWheelValue < Input.PreviousMouseState.ScrollWheelValue)
                    Globals.Scale--;
                else
                    Globals.Scale++;
            }
            else
                Globals.ScaleChanged = false;
        }

        #endregion Zoom

        #region Pencil

        /// <summary>
        /// Colors the cell at the given point.
        /// </summary>
        /// <param name="point">The screen x,y coordinate for the cell to be colored.</param>
        /// <param name="color">The color to make the cell.</param>
        public void ColorCell(Point point, Color color)
        {
            foreach (GridCell cell in FrameManager.ActiveFrame.Grid.Peek().Cells)
            {
                if (cell.Bounds.Contains(point))
                {
                    cell.Color = color;
                    break;
                }
            }
        }

        #endregion Pencil

        #region Line

        /// <summary>
        /// Creates a line of colored cells on the grid.
        /// </summary>
        /// <param name="start">The window based location of the beginning of the line.</param>
        /// <param name="end">The window based location of the end of the line.</param>
        public void createLine(Vector2 start, Vector2 end)
        {

            //Grid grid = FrameManager.ActiveFrame.Grid.Peek().Clone();

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.Peek().GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.Peek().GridSize.Y; y++)
                {
                    if (Physics.LineIntersectsRect(start.ToPoint(), end.ToPoint(), FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds))
                    {
                        FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Color = Globals.DrawingColor;
                        //grid.Cells[x, y].Color = Globals.DrawingColor;
                    }
                }
            }

            //FrameManager.ActiveFrame.Grid.Push(grid);
        }

        #endregion Line


        #region Rectangle

        private void createRectangle(Vector2 start, Vector2 end)
        {
            createLine(start, new Vector2(start.X, end.Y));
            createLine(new Vector2(start.X, end.Y), end);
            createLine(end, new Vector2(end.X, start.Y));
            createLine(new Vector2(end.X, start.Y), start);

            //for (int x = 0; x < FrameManager.ActiveFrame.Grid.GridSize.X; x++)
            //{
            //    for (int y = 0; y < FrameManager.ActiveFrame.Grid.GridSize.Y; y++)
            //    {
            //        if (Physics.LineIntersectsRect(start.ToPoint(), end.ToPoint(), FrameManager.ActiveFrame.Grid.Cells[x, y].Bounds))
            //        {
            //            FrameManager.ActiveFrame.Grid.Cells[x, y].Color = Globals.DrawingColor;
            //        }
            //    }
            //}
        }

        private void createCircle(Vector2 start, Vector2 end)
        {
            // TODO: Handle Circle
            // 1. Find center of Circle
            // x,y are coordinates of any point on the ellipse,
            // a,b are the radius on the x and y axes respectively,
            // t is the parameter, which ranges from 0 to 2pi radians

            // x = a cos t
            // y = b sin t
            //Vector2 drawEnd = 
            Vector2 center = new Rectangle((int)start.X, (int)start.Y, (int)end.X - (int)start.X, (int)end.Y - (int)start.Y).Center.ToVector2();
            int a = Math.Abs((int)(start.X - end.X) / 2);
            int b = Math.Abs((int)(start.Y - end.Y) / 2);

            circlePoints.Clear();

            double theta = 0;
            int h = (int)center.X;
            int k = (int)center.Y;
            double step = 15 * Math.PI/180;

            while(theta < MathHelper.TwoPi)
            {
                int x = (int)(h + (a * Math.Cos(theta)));
                int y = (int)(k + (b * Math.Sin(theta)));
                circlePoints.Add(new Vector2(x, y));
                theta += step;
            }

            for(int i = 0; i < circlePoints.Count - 1; i++)
            {
                createLine(circlePoints[i], circlePoints[i + 1]);
            }
            createLine(circlePoints[circlePoints.Count - 1], circlePoints[0]);
        }

        List<Vector2> circlePoints = new List<Vector2>();

        #endregion Rectangle
        #region Pan

        private void Pan(Vector2 delta)
        {
            FrameManager.ActiveFrame.Grid.Peek().Position = FrameManager.ActiveFrame.Grid.Peek().Position + delta;
            FrameManager.ActiveFrame.Grid.Peek().RebuildGrid();
        }

        #endregion Pan

        #region Filling

        private void FillGrid(GridCell gridCell)
        {
            Color changeColor = gridCell.Color;

            GridCheckStruct[,] checkStruct = new GridCheckStruct[(int)FrameManager.ActiveFrame.Grid.Peek().GridSize.X, (int)FrameManager.ActiveFrame.Grid.Peek().GridSize.Y];

            InitializeGridCheckStructArrayArray(checkStruct);

            checkStruct[(int)gridCell.ID.X, (int)gridCell.ID.Y].Change = true;

            FindFillCells(ref changeColor, ref checkStruct);

            FillCells(checkStruct);
        }

        private static void FillCells(GridCheckStruct[,] checkStruct)
        {
            foreach (GridCheckStruct check in checkStruct)
            {
                if (check.Change)
                {
                    FrameManager.ActiveFrame.Grid.Peek().Cells[(int)check.ID.X, (int)check.ID.Y].Color = Globals.DrawingColor;
                }
            }
        }

        private static void FindFillCells(ref Color changeColor, ref GridCheckStruct[,] checkStruct)
        {
            Boolean allChecked = false;
            while (!allChecked)
            {
                allChecked = true;

                GridCheckStruct[,] holdStruct = checkStruct;

                foreach (GridCheckStruct index in checkStruct)
                {
                    if (index.Change && !index.AllChecked)
                    {
                        if ((int)index.ID.X > 0)
                            if (!index.LeftChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Peek().Cells[(int)index.ID.X - 1, (int)index.ID.Y].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X - 1, (int)index.ID.Y].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].LeftChecked = true;
                            }
                        if (index.ID.X < FrameManager.ActiveFrame.Grid.Peek().GridSize.X - 1)
                            if (!index.RightChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Peek().Cells[(int)index.ID.X + 1, (int)index.ID.Y].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X + 1, (int)index.ID.Y].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].RightChecked = true;
                            }
                        if (index.ID.Y > 0)
                            if (!index.UpChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Peek().Cells[(int)index.ID.X, (int)index.ID.Y - 1].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X, (int)index.ID.Y - 1].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].UpChecked = true;
                            }
                        if (index.ID.Y < FrameManager.ActiveFrame.Grid.Peek().GridSize.Y - 1)
                            if (!index.DownChecked)
                            {
                                allChecked = false;
                                if (FrameManager.ActiveFrame.Grid.Peek().Cells[(int)index.ID.X, (int)index.ID.Y + 1].Color == changeColor)
                                {
                                    holdStruct[(int)index.ID.X, (int)index.ID.Y + 1].Change = true;
                                }
                                holdStruct[(int)index.ID.X, (int)index.ID.Y].DownChecked = true;
                            }
                    }
                }

                checkStruct = holdStruct;
            }
        }

        private void InitializeGridCheckStructArrayArray(GridCheckStruct[,] checkStruct)
        {
            for (int x = 0; x < (int)this.grid.GridSize.X; x++)
            {
                for (int y = 0; y < (int)this.grid.GridSize.Y; y++)
                {
                    checkStruct[x, y].Change = false;
                    checkStruct[x, y].LeftChecked = false;
                    checkStruct[x, y].RightChecked = false;
                    checkStruct[x, y].UpChecked = false;
                    checkStruct[x, y].DownChecked = false;
                    checkStruct[x, y].ID = new Vector2(x, y);
                }
            }
        }

        #endregion Filling
        #endregion Handler Helpers


        #region Helper Methods
        /// <summary>
        /// Returns the GridCell that the mouse is over.
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        private GridCell getMouseOverGridCell(Point mousePosition)
        {
            GridCell gc = new GridCell(new Vector2(0, 0), Color.White, new Vector2(0, 0));

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.Peek().GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.Peek().GridSize.Y; y++)
                {
                    if (FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds.Contains(mousePosition))
                        return FrameManager.ActiveFrame.Grid.Peek().Cells[x, y];
                }
            }

            return gc;
        }


        /// <summary>
        /// Returns the Center of the cell that the mouse is over.
        /// </summary>
        /// <param name="mousePosition">Position of the mouse.</param>
        /// <returns></returns>
        public Vector2 MouseDrawPoint(Point mousePosition)
        {
            Vector2 retVal = Vector2.Zero;

            for (int x = 0; x < FrameManager.ActiveFrame.Grid.Peek().GridSize.X; x++)
            {
                for (int y = 0; y < FrameManager.ActiveFrame.Grid.Peek().GridSize.Y; y++)
                {
                    if (FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds.Contains(mousePosition))
                    {
                        return FrameManager.ActiveFrame.Grid.Peek().Cells[x, y].Bounds.Center.ToVector2();
                    }
                }
            }

            return retVal;
        }

        #endregion Helper Methods
    }
}
