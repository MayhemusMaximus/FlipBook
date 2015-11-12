
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace FlipBook
{
    public class AnimationScreen : BaseScreen
    {
        Texture2D border;

        public AnimationScreen(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;

            buildTexture();
        }

        private void buildTexture()
        {
            border = new Texture2D(Globals.GraphicsDevice, 1, 1);
            Texture hold = Globals.GraphicsDevice.Textures[0];
            Globals.GraphicsDevice.Textures[0] = null;

            Color[] colors = new Color[1];
            colors[0] = Color.White;

            border.SetData(colors);
            Globals.GraphicsDevice.Textures[0] = hold;

        }

        private int frameNDX = 0;
        private int elapsedTime = 0;
        public override void Update()
        {
            elapsedTime += Globals.GameTime.ElapsedGameTime.Milliseconds;
            if(elapsedTime >= FrameManager.Frames[frameNDX].Time)
            {
                elapsedTime -= FrameManager.Frames[frameNDX].Time;

                frameNDX++;
                if (frameNDX >= FrameManager.Frames.Count)
                    frameNDX = 0;
            }
        }

        public override void Draw()
        {
            //Globals.SpriteBatch.Draw(this.border, this.Bounds, Color.WhiteSmoke);
            Globals.SpriteBatch.Draw(FrameManager.Frames[frameNDX].Grid.ToTexture2D(), this.Bounds, Color.White);
        }
    }
}
