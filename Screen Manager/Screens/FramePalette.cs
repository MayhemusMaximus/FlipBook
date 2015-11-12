using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlipBook
{
    public class FramePalette : BaseScreen
    {
        Texture2D texture;

        public FramePalette(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;

            BuildTexture();
        }

        private void BuildTexture()
        {
            texture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[1];
            colors[0] = Color.White;

            texture.SetData(colors);

            Globals.GraphicsDevice.Textures[0] = hold;

        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Textures.texture, this.Bounds, Color.DarkGray);

            int ndx = 1;
            foreach(Frame frame in FrameManager.Frames)
            {
                Color frameBorderColor = Color.Wheat;
                if (frame.IsActive)
                    frameBorderColor = Color.Lavender;
                Globals.SpriteBatch.Draw(texture, new Rectangle(this.Bounds.X + ((this.Bounds.Height - 10) * (frame.Sequence - 1)) + (5 * ndx), this.Bounds.Y + 5, this.Bounds.Height - 10, this.Bounds.Height - 10), frameBorderColor);
                Globals.SpriteBatch.Draw(frame.Grid.ToTexture2D(), new Rectangle(this.Bounds.X + ((this.Bounds.Height - 20) * (frame.Sequence - 1)) + (10 * ndx), this.Bounds.Y + 10, this.Bounds.Height - 20, this.Bounds.Height - 20), Color.White);
                ndx++;
            }
        }
    }
}
