using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlipBook
{
    public class ArraySwatch : BaseScreen
    {
        Texture2D swatch;

        ArraySwatchSelector ass;

        private Color verticalSwatchColor = Color.Orange;
        public Color VerticalSwatchColor
        {
            get { return verticalSwatchColor; }
            set
            {
                if (verticalSwatchColor != value)
                {
                    verticalSwatchColor = value;
                    BuildSwatch(value);
                    SetColorByPoint = ass.SelectionPoint;
                }
            }
        }

        //public Color CurrentColor { get; private set; }

        public Color SelectByColor
        {
            get;
            set;
        }

        public Vector2 SetColorByPoint
        {
            set
            {
                //CurrentColor = getColorFromPoint(value);
                Globals.DrawingColor = getColorFromPoint(value);
            }
        }

        private Color getColorFromPoint(Vector2 mouse)
        {
            Vector2 incrementalMouse = new Vector2(mouse.X - Bounds.X, mouse.Y - Bounds.Y);

            double start;

            double rScaleY;
            double gScaleY;
            double bScaleY;

            double rEnd;
            double gEnd;
            double bEnd;

            double rDifference;
            double gDifference;
            double bDifference;

            double rScaleX;
            double gScaleX;
            double bScaleX;

            int ndx = 0;
            for (int y = 0; y < this.Size.Y; y++)
            {
                start = 256 - y;

                rScaleY = (double)verticalSwatchColor.R / 256;
                gScaleY = (double)verticalSwatchColor.G / 256;
                bScaleY = (double)verticalSwatchColor.B / 256;

                rEnd = (double)verticalSwatchColor.R - (rScaleY * y);
                gEnd = (double)verticalSwatchColor.G - (gScaleY * y);
                bEnd = (double)verticalSwatchColor.B - (bScaleY * y);

                rDifference = start - rEnd;
                gDifference = start - gEnd;
                bDifference = start - bEnd;

                rScaleX = rDifference / 256;
                gScaleX = gDifference / 256;
                bScaleX = bDifference / 256;

                for (int x = 0; x < this.Size.X; x++)
                {
                    int r = (int)(start - (x * rScaleX));
                    int g = (int)(start - (x * gScaleX));
                    int b = (int)(start - (x * bScaleX));

                    if(new Vector2(x,y) == incrementalMouse)
                        return new Color(r, g, b);

                    //colors[ndx] = new Color(r, g, b);

                    ndx++;
                }
            }
            return Color.Gray;
        }

        public ArraySwatch()
        {
            this.Name = "Array Swatch";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(256, 256);

            ass = new ArraySwatchSelector(new Vector2(Bounds.Right - Bounds.X - 5, Bounds.Bottom - Bounds.Y - 5), new Vector2(10, 10));
            BuildSwatch(Color.Orange);
        }

        public ArraySwatch(Vector2 position, Vector2 size)
        {
            this.Name = "Array Swatch";
            this.Position = position;
            this.Size = size;

            ass = new ArraySwatchSelector(new Vector2(Bounds.X + (Bounds.Width / 2) - 5, Bounds.Y + (Bounds.Height / 2) - 5), new Vector2(10, 10));
            BuildSwatch(Color.Orange);
            SetColorByPoint = ass.SelectionPoint;
        }

        public override void Update()
        {
            if (this.Bounds.Contains(Input.CurrentMousePosition) && Input.CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                ass.SelectionPoint = Input.CurrentMousePosition.ToVector2();
                SetColorByPoint = Input.CurrentMousePosition.ToVector2();
            }
            ass.Update();
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(swatch, Bounds, Color.White);
            ass.Draw();
        }

        private void BuildSwatch(Color color)
        {
            //CurrentColor = color;
            //CurrentColor = getColorFromPoint(ass.SelectionPoint);
            //Globals.DrawingColor = CurrentColor;

            swatch = new Texture2D(Globals.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);

            Texture hold;
            hold = Globals.GraphicsDevice.Textures[0];

            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[(int)this.Size.X * (int)this.Size.Y];
            swatch.GetData(colors);

            double start;

            double rScaleY;
            double gScaleY;
            double bScaleY;

            double rEnd;
            double gEnd;
            double bEnd;

            double rDifference;
            double gDifference;
            double bDifference;

            double rScaleX;
            double gScaleX;
            double bScaleX;

            int ndx = 0;
            for (int y = 0; y < this.Size.Y; y++)
            {
                start = 256 - y;

                rScaleY = (double)color.R / 256;
                gScaleY = (double)color.G / 256;
                bScaleY = (double)color.B / 256;

                rEnd = (double)color.R - (rScaleY * y);
                gEnd = (double)color.G - (gScaleY * y);
                bEnd = (double)color.B - (bScaleY * y);

                rDifference = start - rEnd;
                gDifference = start - gEnd;
                bDifference = start - bEnd;

                rScaleX = rDifference / 256;
                gScaleX = gDifference / 256;
                bScaleX =  bDifference / 256;

                for (int x = 0; x < this.Size.X; x++)
                {
                    int r = (int)(start - (x * rScaleX));
                    int g = (int)(start - (x * gScaleX));
                    int b = (int)(start - (x * bScaleX));

                    Color c = new Color(r, g, b);

                    colors[ndx] = new Color(r, g, b);

                    ndx++;
                }
            }

                swatch.SetData(colors);

            Globals.GraphicsDevice.Textures[0] = hold;
        }
    }
}
