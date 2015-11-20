using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FlipBook
{
    public class FramePalette : BaseScreen
    {
        List<Rectangle> Thumbnails = new List<Rectangle>();

        Toolbar toolbar = new Toolbar();

        public FramePalette(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;

            toolbar.Position = new Vector2(this.Bounds.X, this.Bounds.Y + 2);

            buildThumbnails();

            Button btnNewFrame = new Button(new Vector2(this.Bounds.X + 5, this.Bounds.Y + 5), new Vector2(20, 20));
            btnNewFrame.Image = Textures.AddFrame;
            btnNewFrame.IsSelected = false;
            btnNewFrame.Name = "New Frame";

            Button btnDeleteFrame = new Button(new Vector2(this.Bounds.X + 5 + btnNewFrame.Bounds.Width, this.Bounds.Y + 5), new Vector2(20, 20));
            btnDeleteFrame.Image = Textures.DeleteFrame;
            btnDeleteFrame.IsSelected = false;
            btnDeleteFrame.Name = "Delete Frame";

            toolbar.Buttons.Add(btnNewFrame);
            toolbar.Buttons.Add(btnDeleteFrame);
        }

        private void buildThumbnails()
        {
            Thumbnails.Clear();
            int ndx = 1;
            foreach (Frame frame in FrameManager.Frames)
            {
                Thumbnails.Add(new Rectangle(this.Bounds.X + ((this.Bounds.Height - 10 - 30) * (frame.Sequence - 1)) + (5 * ndx), this.Bounds.Y + 5 + 30, this.Bounds.Height - 10 - 30, this.Bounds.Height - 10 - 30));
                ndx++;
            }
        }

        public override void Update()
        {
            if(((Button)"New Frame".ToButton(toolbar.Buttons)).Clicked)
            {
                FrameManager.AddFrame();
            }


            buildThumbnails();

            toolbar.Update();
            int ndx = 0;
            foreach(Rectangle thumbnail in Thumbnails)
            {
                if(thumbnail.Contains(Input.CurrentMousePosition))
                {
                    if(Input.CurrentMouseState.LeftButton == ButtonState.Pressed && Input.PreviousMouseState.LeftButton == ButtonState.Released)
                    {
                        FrameManager.ActiveFrame = FrameManager.Frames[ndx];
                        break;
                    }
                }
                ndx++;
            }
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Textures.texture, this.Bounds, Color.DarkGray);

            toolbar.Draw();

            int ndx = 1;
            foreach(Frame frame in FrameManager.Frames)
            {
                Color frameBorderColor = Color.Wheat;
                if (frame.IsActive)
                    frameBorderColor = Color.Black;

                Globals.SpriteBatch.Draw(Textures.SimpleTexture, Thumbnails[ndx - 1], frameBorderColor);

                Rectangle imageBounds = new Rectangle(Thumbnails[ndx - 1].X + 5, Thumbnails[ndx - 1].Y + 5, Thumbnails[ndx - 1].Width - 10, Thumbnails[ndx - 1].Height - 10);
                Globals.SpriteBatch.Draw(frame.Grid.Peek().ToTexture2D(), imageBounds, Color.White);
                ndx++;
            }
        }
    }
}
