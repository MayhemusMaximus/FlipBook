using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlipBook
{
    public class ArraySwatchSelector : BaseScreen
    {
        Texture2D selectorTexture;
        public Vector2 SelectionPoint
        {
            get
            {
                return new Vector2(this.Position.X + (this.Size.X / 2), this.Position.Y + (this.Size.Y / 2));
            }
            set
            {
                this.Position = new Vector2(value.X - (this.Size.X / 2), value.Y - (this.Size.Y / 2));
            }
        }

        public ArraySwatchSelector()
        {
            this.Name = "Array Swatch Selector";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(20, 20);

            BuildSelectorTexture();
        }

        public ArraySwatchSelector(Vector2 position, Vector2 size)
        {
            this.Name = "Array Swatch Selector";
            this.Position = position;
            this.Size = size;

            BuildSelectorTexture();
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(selectorTexture, new Rectangle(this.Bounds.X, this.Bounds.Y, 10,10), Color.White);
        }

        private void BuildSelectorTexture()
        {
            Vector2 textureSize = new Vector2(this.Size.X * 2, this.Size.Y * 2);
            selectorTexture = new Texture2D(Globals.GraphicsDevice, (int)textureSize.X, (int)textureSize.Y);
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[(int)textureSize.X * (int)textureSize.Y];
            selectorTexture.GetData(colors);

            Vector2 center = new Vector2(textureSize.X / 2, textureSize.Y / 2);
            int ndx = 0;
            for (int y = 0; y < textureSize.Y; y++)
            {
                for (int x = 0; x < textureSize.X; x++)
                {
                    Vector2 location = new Vector2(x, y);
                    float radius = (center - location).Length();
                    if (radius + 3 >= textureSize.X / 2 && radius <= textureSize.X / 2)
                        colors[ndx] = Color.Black;
                    ndx++;
                }
            }

                selectorTexture.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;
        }
    }
}
