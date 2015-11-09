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
        Line
    }

    public class Toolbar : BaseScreen
    {
        Texture2D texture;

        Button btnPencil;
        Button btnEraser;
        Button btnLine;

        public DrawMode DrawMode { get; private set; }

        public Toolbar()
        {
            this.Name = "Toolbar";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(Globals.Graphics.PreferredBackBufferWidth, 30);

            btnPencil = new Button(new Vector2(this.Bounds.X + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnPencil.Image = Textures.Pencil;

            btnEraser = new Button(new Vector2(btnPencil.Bounds.X + btnPencil.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnEraser.Image = Textures.Eraser;

            btnLine = new Button(new Vector2(btnEraser.Bounds.X + btnEraser.Bounds.Width + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnLine.Image = Textures.Line;

            this.DrawMode = DrawMode.Pencil;

            BuildToolBarTexture();
        }

        private void BuildToolBarTexture()
        {
            texture = new Texture2D(Globals.GraphicsDevice, 1, 1);

            Texture hold = Globals.GraphicsDevice.Textures[0];

            Color[] colors = new Color[texture.Width * texture.Height];
            texture.GetData(colors);

            int ndx = 0;
            for (int y = 0; y < texture.Height; y++ )
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    colors[ndx] = Color.Wheat;
                    ndx++;
                }
            }

            texture.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;
        }

        public override void Unload()
        {
            
        }

        public override void Update()
        {
            btnPencil.Update();
            btnEraser.Update();
            btnLine.Update();

            if (btnPencil.Clicked)
            {
                this.DrawMode = DrawMode.Pencil;
                btnPencil.IsSelected = true;

                btnEraser.IsSelected = false;
                btnLine.IsSelected = false;
            }
            if (btnEraser.Clicked)
            {
                this.DrawMode = DrawMode.Eraser;
                btnEraser.IsSelected = true;

                btnPencil.IsSelected = false;
                btnLine.IsSelected = false;
            }
            if(btnLine.Clicked)
            {
                this.DrawMode = DrawMode.Line;
                btnLine.IsSelected = true;

                btnEraser.IsSelected = false;
                btnPencil.IsSelected = false;
            }
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, this.Bounds, Color.White);

            btnPencil.Draw();
            btnEraser.Draw();
            btnLine.Draw();
        }
    }
}
