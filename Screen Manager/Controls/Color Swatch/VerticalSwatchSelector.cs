using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlipBook
{
    public class VerticalSwatchSelector : BaseScreen
    {
        private int point;

        public int Point
        {
            get { return point; }
            set
            {
                point = value;
                Position = new Vector2(this.Position.X, point - ((int)Size.Y / 2));
            }
        }

        private Texture2D colorSelector;

        public VerticalSwatchSelector()
        {
            Name = "Vertical Swatch Selector";
            Position = new Vector2(0, 0);
            Size = new Vector2(5, 10);

            BuildColorSelector();
        }

        public VerticalSwatchSelector(Vector2 position, Vector2 size)
        {
            this.Name = "Vertical Swatch Selector";
            this.Position = position;
            this.Size = size;

            BuildColorSelector();
        }

        public VerticalSwatchSelector(String name, Vector2 position, Vector2 size)
        {
            this.Name = name;
            this.Position = position;
            this.Size = size;

            BuildColorSelector();
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(colorSelector, new Rectangle(Bounds.X, Bounds.Y, 5, 10), Color.White);
        }

        private void BuildColorSelector()
        {
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Vector2 selectorSize = new Vector2(5, 10);
            colorSelector = new Texture2D(Globals.GraphicsDevice, (int)selectorSize.X, (int)selectorSize.Y);

            Color[] colors = new Color[(int)selectorSize.X * (int)selectorSize.Y];
            colorSelector.GetData(colors);

            int ndx = 0;
            for (int y = 0; y < selectorSize.Y; y++)
            {
                for (int x = 0; x < selectorSize.X; x++)
                {
                    if (y < selectorSize.Y / 2)
                    {
                        if (x <= y)
                            colors[ndx] = Color.Black;
                    }
                    else
                    {
                        colors[ndx] = Color.Black;
                        if (x >= selectorSize.Y - y)
                            colors[ndx] = Color.Transparent;
                    }

                    ndx++;
                }
            }

            colorSelector.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;
        }
    }
}