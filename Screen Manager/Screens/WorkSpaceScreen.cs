using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlipBook
{
    public class WorkSpaceScreen : BaseScreen
    {
        private Toolbar toolbar = new Toolbar();
        private Swatch swatch = new Swatch("Swatch", new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, 30), new Vector2(300, Globals.Graphics.PreferredBackBufferHeight - 200));
        private PaintScreen paintScreen = new PaintScreen(new Vector2(0,30), new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, Globals.Graphics.PreferredBackBufferHeight - 200 - 30));
        private FramePalette framePalette = new FramePalette(new Vector2(0, Globals.Graphics.PreferredBackBufferHeight - 200), new Vector2(Globals.Graphics.PreferredBackBufferWidth, 200));
        private AnimationScreen animationScreen = new AnimationScreen(new Vector2(Globals.Graphics.PreferredBackBufferWidth - Globals.ImageSize.X, Globals.Graphics.PreferredBackBufferHeight - 200), Globals.ImageSize);


        //Button btnPencil;
        //Button btnEraser;
        //Button btnLine;
        //Button btnShowGrid;

        public WorkSpaceScreen(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;

            Button btnPencil = new Button(new Vector2(this.Bounds.X + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnPencil.Image = Textures.Pencil;
            btnPencil.IsSelected = true;
            btnPencil.Name = "Pencil";

            Button btnEraser = new Button(new Vector2(btnPencil.Bounds.X + btnPencil.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnEraser.Image = Textures.Eraser;
            btnEraser.IsSelected = false;
            btnEraser.Name = "Eraser";

            Button btnLine = new Button(new Vector2(btnEraser.Bounds.X + btnEraser.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnLine.Image = Textures.Line;
            btnLine.IsSelected = false;
            btnLine.Name = "Line";

            Button btnShowGrid = new Button(new Vector2(btnLine.Bounds.X + btnLine.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnShowGrid.Image = Textures.Grid;
            btnShowGrid.IsSelected = true;
            btnShowGrid.Name = "ShowGrid";

            toolbar.Buttons.Add(btnPencil);
            toolbar.Buttons.Add(btnEraser);
            toolbar.Buttons.Add(btnLine);
            toolbar.Buttons.Add(btnShowGrid);

            //if(((Button)"ShowGrid".ToButton(toolbar.Buttons)).IsSelected)
            //{

            //}
        }

        public override void Update()
        {
            //if (paintScreen.Bounds.Contains(Input.CurrentMousePosition))
            //    Globals.MouseIsVisible = false;
            //else
            //    Globals.MouseIsVisible = true;

            if (((Button)"Pencil".ToButton(toolbar.Buttons)).Clicked)
            {
                Globals.DrawMode = DrawMode.Pencil;

                toolbar.Select("Pencil", true);
                toolbar.Select("Eraser", false);
                toolbar.Select("Line", false);
            }
            if (((Button)"Eraser".ToButton(toolbar.Buttons)).Clicked)
            {
                Globals.DrawMode = DrawMode.Eraser;

                toolbar.Select("Eraser", true);
                toolbar.Select("Pencil", false);
                toolbar.Select("Line", false);
            }
            if (((Button)"Line".ToButton(toolbar.Buttons)).Clicked)
            {
                Globals.DrawMode = DrawMode.Line;
                //btnLine.IsSelected = true;
                //btnEraser.IsSelected = false;
                //btnPencil.IsSelected = false;

                toolbar.Select("Line", true);
                toolbar.Select("Pencil", false);
                toolbar.Select("Eraser", false);

            }
            if (((Button)"ShowGrid".ToButton(toolbar.Buttons)).Clicked)
            {
                toolbar.Select("ShowGrid", !((Button)"ShowGrid".ToButton(toolbar.Buttons)).IsSelected);
                //btnShowGrid.IsSelected = !btnShowGrid.IsSelected;
                Globals.ShowGrid = ((Button)"ShowGrid".ToButton(toolbar.Buttons)).IsSelected;
            }

            paintScreen.Update();

            swatch.Update();

            toolbar.Update();
            framePalette.Update();
            animationScreen.Update();
        }

        public override void Draw()
        {
            paintScreen.Draw();
            toolbar.Draw();
            swatch.Draw();
            framePalette.Draw();
            animationScreen.Draw();
        }
    }
}