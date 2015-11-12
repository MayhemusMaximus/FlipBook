﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlipBook
{
    public class WorkSpaceScreen : BaseScreen
    {
        // TODO: ADD FRAMES PALETTE TO WORKSPACE

        private Toolbar toolbar = new Toolbar();
        private Swatch swatch = new Swatch("Swatch", new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, 30), new Vector2(300, Globals.Graphics.PreferredBackBufferHeight - 200));
        //private Grid grid = new Grid(new Vector2(0, 30), new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, Globals.Graphics.PreferredBackBufferHeight - 200 - 30), new Vector2(32, 32));
        private PaintScreen paintScreen = new PaintScreen(new Vector2(0,30), new Vector2(Globals.Graphics.PreferredBackBufferWidth - 300, Globals.Graphics.PreferredBackBufferHeight - 200 - 30));
        private FramePalette framePalette = new FramePalette(new Vector2(0, Globals.Graphics.PreferredBackBufferHeight - 200), new Vector2(Globals.Graphics.PreferredBackBufferWidth, 200));
        private AnimationScreen animationScreen = new AnimationScreen(new Vector2(Globals.Graphics.PreferredBackBufferWidth - Globals.ImageSize.X, Globals.Graphics.PreferredBackBufferHeight - 200), Globals.ImageSize);

        // TODO: EXPLORE MAKELINE()
        //private Texture2D line = new Texture2D(Globals.GraphicsDevice, 1, 200);
        //public void makeLine()
        //{
        //    Color[] colors = new Color[line.Width * line.Height];
        //    line.GetData(colors);

        //    for(int x = 0; x < colors.Count(); x++)
        //    {
        //        colors[x] = Color.Red;
        //    }

        //    line.SetData(colors);
        //}

        public WorkSpaceScreen(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;

            //makeLine();
        }

        public override void Update()
        {
            //if (grid.Bounds.Contains(Input.CurrentMousePosition))
            //    Globals.MouseIsVisible = false;
            //else
            //    Globals.MouseIsVisible = true;
            if (paintScreen.Bounds.Contains(Input.CurrentMousePosition))
                Globals.MouseIsVisible = false;
            else
                Globals.MouseIsVisible = true;

            //grid.DrawColor = swatch.PencilColor;
            //grid.DrawMode = toolbar.DrawMode;
            //grid.ShowGridLines = toolbar.ShowGrid;

            //grid.Update();
            paintScreen.Update();

            swatch.Update();

            toolbar.Update();
            animationScreen.Update();
        }

        public override void Draw()
        {
            //grid.Draw();
            paintScreen.Draw();
            toolbar.Draw();
            swatch.Draw();
            framePalette.Draw();
            animationScreen.Draw();
        }
    }
}