using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlipBook
{
    public class Swatch : BaseScreen
    {
        //public Color CurrentColor;
        private VerticalSwatch vs;
        private ArraySwatch arraySwatch;

        private Texture2D simpleTexture;

        public Color PencilColor { get; private set; }

        private void BuildTexture()
        {
            simpleTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);

            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[simpleTexture.Width * simpleTexture.Height];

            simpleTexture.GetData(colors);

            int ndx = 0;
            for (int y = 0; y < simpleTexture.Height; y++)
            {
                for (int x = 0; x < simpleTexture.Width; x++)
                {
                    colors[ndx] = Color.White;
                    ndx++;
                }
            }

                simpleTexture.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;
        }

        //public Swatch()
        //{
        //    Name = "Swatch Control";
        //    Position = new Vector2(200, 200);
        //    Size = new Vector2(15, 200);
        //    BuildTexture();
        //    //vs = new VerticalSwatch(new Vector2(200, 100), new Vector2(15, 200));
        //    //arraySwatch = new 
        //}

        public Swatch(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;

            //vs.SelectedColor = new Color(125,0,200);
            arraySwatch = new ArraySwatch(new Vector2(this.Bounds.X + 5, this.Bounds.Y + 25), new Vector2(256, 256));
            arraySwatch.VerticalSwatchColor = Globals.DrawingColor;
            //arraySwatch.CurrentColor = Globals.DrawingColor;

            vs = new VerticalSwatch(new Vector2(arraySwatch.Bounds.Right + 5, arraySwatch.Bounds.Y), new Vector2(20,256));
            vs.SelectByColor = Color.Orange;
            BuildTexture();
            //vss = new VerticalSwatchSelector(new Vector2(198, 145), new Vector2(19, 10));
        }

        public override void Update()
        {
            vs.Update();
            arraySwatch.VerticalSwatchColor = vs.CurrentColor;
            arraySwatch.Update();
            //PencilColor = arraySwatch.CurrentColor;
            PencilColor = Globals.DrawingColor;
        }

        public override void Draw()
        {
            // Draw Palette
            Globals.SpriteBatch.Draw(simpleTexture, this.Bounds, Color.Gray);

            vs.Draw();
            arraySwatch.Draw();

            //Globals.SpriteBatch.Draw(simpleTexture, new Rectangle((int)this.Bounds.X + 5, (int)this.Bounds.Y + 5, 256, 15), arraySwatch.CurrentColor);
            Globals.SpriteBatch.Draw(simpleTexture, new Rectangle((int)this.Bounds.X + 5, (int)this.Bounds.Y + 5, 256, 15), Globals.DrawingColor);
        }
    }
}
