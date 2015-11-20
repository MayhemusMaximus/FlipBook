
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace FlipBook
{
    public class AnimationScreen : BaseScreen
    {

        public AnimationScreen(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
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
            Globals.SpriteBatch.Draw(FrameManager.Frames[frameNDX].Grid.Peek().ToTexture2D(), this.Bounds, Color.White);
        }
    }
}
