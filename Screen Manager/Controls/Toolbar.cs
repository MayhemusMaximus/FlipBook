using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public enum DrawMode
    {
        Pencil,
        Eraser,
        Line,
        Fill,
        Rectangle
    }

    public class Toolbar : BaseScreen
    {

        public List<Button> Buttons = new List<Button>();

        //Button btnPencil;
        //Button btnEraser;
        //Button btnLine;
        //Button btnShowGrid;

        public Toolbar()
        {
            this.Name = "Toolbar";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(Globals.Graphics.PreferredBackBufferWidth, 30);

            //btnPencil = new Button(new Vector2(this.Bounds.X + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            //btnPencil.Image = Textures.Pencil;
            //btnPencil.IsSelected = true;

            //btnEraser = new Button(new Vector2(btnPencil.Bounds.X + btnPencil.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            //btnEraser.Image = Textures.Eraser;

            //btnLine = new Button(new Vector2(btnEraser.Bounds.X + btnEraser.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            //btnLine.Image = Textures.Line;

            //btnShowGrid = new Button(new Vector2(btnLine.Bounds.X + btnLine.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            //btnShowGrid.Image = Textures.Grid;
            //btnShowGrid.IsSelected = true;

            //this.DrawMode = DrawMode.Pencil;
        }

        public override void Unload()
        {
            
        }

        public void Select(String button, Boolean select)
        {
            foreach(Button btn in Buttons)
            {
                if(button == btn.Name)
                {
                    btn.IsSelected = select;
                    return;
                }
            }
        }

        public override void Update()
        {
            foreach(Button button in Buttons)
            {
                button.Update();
            }
            //btnPencil.Update();
            //btnEraser.Update();
            //btnLine.Update();
            //btnShowGrid.Update();

            //if (btnPencil.Clicked)
            //{
            //    Globals.DrawMode = DrawMode.Pencil;
            //    btnPencil.IsSelected = true;

            //    btnEraser.IsSelected = false;
            //    btnLine.IsSelected = false;
            //}
            //if (btnEraser.Clicked)
            //{
            //    Globals.DrawMode = DrawMode.Eraser;
            //    btnEraser.IsSelected = true;

            //    btnPencil.IsSelected = false;
            //    btnLine.IsSelected = false;
            //}
            //if(btnLine.Clicked)
            //{
            //    Globals.DrawMode = DrawMode.Line;
            //    btnLine.IsSelected = true;

            //    btnEraser.IsSelected = false;
            //    btnPencil.IsSelected = false;
            //}
            //if(btnShowGrid.Clicked)
            //{
            //    btnShowGrid.IsSelected = !btnShowGrid.IsSelected;
            //    Globals.ShowGrid = btnShowGrid.IsSelected;
            //}

            //ShowGrid = btnShowGrid.IsSelected;

        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Textures.SimpleTexture, this.Bounds, Color.Wheat);

            foreach(Button button in Buttons)
            {
                button.Draw();
            }

            //btnPencil.Draw();
            //btnEraser.Draw();
            //btnLine.Draw();
            //btnShowGrid.Draw();
        }
    }
}
