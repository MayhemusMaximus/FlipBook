using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlipBook
{


    public class VerticalSwatch : BaseScreen
    {
        private Texture2D texture;
        VerticalSwatchSelector vss;

        public Color CurrentColor { get; private set; }

        public Color SelectByColor
        {
            set
            {
                CurrentColor = value;
                Double p = (Double)getPointFromColor(value);
                vss.Point = (int)(((double)p / (256 * 6)) * (int)this.Size.Y) + (int)this.Position.Y;
            }
        }

        public int SelectByPoint
        {
            set
            {
                CurrentColor = getColorFromPoint(value);
            }
        }

        private Color getColorFromPoint(int mouseY)
        {
            double divisor = mouseY - this.Bounds.Y;
            double scale = divisor / this.Bounds.Height;
            int loopVal = (int)(scale * 1536);

            Boolean foundPoint = false;
            int y = 0;

            for (int g = 0; g < 256; g++)
            {
                if (loopVal == y)
                {
                    return new Color(255, g, 0);
                }
                y++;
            }

            if (!foundPoint)
                for (int r = 255; r > -1; r--)
                {
                    if (loopVal == y)
                        return new Color(r, 255, 0);
                    y++;
                }

            if (!foundPoint)
                for (int b = 0; b < 256; b++)
                {
                    if (loopVal == y)
                        return new Color(0, 255, b);
                    y++;
                }

            if (!foundPoint)
                for (int g = 255; g > -1; g--)
                {
                    if (loopVal == y)
                        return new Color(0, g, 255);
                    y++;
                }

            if (!foundPoint)
                for (int r = 0; r < 256; r++)
                {
                    if (loopVal == y)
                        return new Color(r, 0, 255);
                    y++;
                }

            if (!foundPoint)
                for (int b = 255; b > -1; b--)
                {
                    if (loopVal == y)
                        return new Color(255, 0, b);
                    y++;
                }

            return Color.White;
        }

        private int getPointFromColor(Color color)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            colors.Sort();

            String s;

            if(colors[2] == color.R)
            {
                if(colors[1] == color.G)
                    s = "RG";
                else
                    s = "RB";
            }
            else if(colors[2] == color.G)
            {
                if(colors[1] == color.R)
                    s = "GR";
                else
                    s = "GB";
            }
            else
            {
                if(colors[1] == color.R)
                    s = "BR";
                else
                    s = "BG";
            }

            Boolean foundPoint = false;
            int y = 0;

            for (int g = 0; g < 256; g++)
            {
                if (s == "RG")
                {
                    if (g == color.G)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            if(!foundPoint)
            for (int r = 255; r > -1; r--)
            {
                if (s == "GR")
                {
                    if (r == color.R)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            if(!foundPoint)
            for (int b = 0; b < 256; b++)
            {
                if (s == "GB")
                {
                    if (b == color.B)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            if(!foundPoint)
            for (int g = 255; g > -1; g--)
            {
                if (s == "BG")
                {
                    if (g == color.G)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            if(!foundPoint)
            for (int r = 0; r < 256; r++)
            {
                if (s == "BR")
                {
                    if (r == color.R)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            if(!foundPoint)
            for (int b = 255; b > -1; b--)
            {
                if (s == "RB")
                {
                    if (b == color.B)
                    {
                        foundPoint = true;
                        break;
                    }
                }
                y++;
            }

            return y;
        }

        private int Zeros(Color color)
        {
            int zeroes = 0;

            if (color.R == 0)
                zeroes++;

            if(color.G == 0)
                zeroes++;

            if(color.B == 0)
                zeroes++;

            return  zeroes;
        }

        public VerticalSwatch(Vector2 position, Vector2 size)
        {
            Name = "Vertical Swatch";
            this.Position = position;
            this.Size = size;

            BuildTexture();
            vss = new VerticalSwatchSelector(new Vector2(this.Position.X - 2, 145), new Vector2(19, 10));
        }

        private void BuildTexture()
        {
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            texture = new Texture2D(Globals.GraphicsDevice, 1, 1536);

            Color[] colors = new Color[1536];
            texture.GetData(colors);

            int y = 0;

            for (int g = 0; g < 256; g++)
            {
                colors[y] = new Color(255, g, 0);
                y++;
            }
            for (int r = 255; r > -1; r--)
            {
                colors[y] = new Color(r, 255, 0);
                y++;
            }
            for (int b = 0; b < 256; b++)
            {
                colors[y] = new Color(0, 255, b);
                y++;
            }
            for (int g = 255; g > -1; g--)
            {
                colors[y] = new Color(0, g, 255);
                y++;
            }
            for (int r = 0; r < 256; r++)
            {
                colors[y] = new Color(r, 0, 255);
                y++;
            }
            for (int b = 255; b > -1; b--)
            {
                colors[y] = new Color(255, 0, b);
                y++;
            }

            texture.SetData(colors);

            Globals.GraphicsDevice.Textures[0] = hold;
        }

        public override void Update()
        {
            if (Bounds.Contains(Input.CurrentMousePosition))
                HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
                UpdateSelectedColor();
        }

        private void UpdateSelectedColor()
        {
            vss.Point = Input.CurrentMousePosition.Y;
            SelectByPoint = Input.CurrentMousePosition.Y;
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Bounds, Color.White);
            vss.Draw();
        }
    }
}
